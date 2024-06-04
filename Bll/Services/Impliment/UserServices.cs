using Bll.Extentions;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Data.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bll.Services.Impliment
{
    public class UserServices : DbServices, IUserServices
    {
        protected readonly IConfiguration _app_settings;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public UserServices(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _app_settings = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetAppSetting(string key) => _app_settings.GetSection(key).Value!;

        public async Task<bool> Register(User user)
        {
            using var db = _connectionData.OpenDbConnection();
            var check_exist = await db.SingleAsync<User>(u => u.email == user.email || u.phone == user.phone);
            if (check_exist != null) { return false; }
            user.password = MD5Services.ComputeMd5Hash(user.password);
            user.id = (int)await db.InsertAsync(user, selectIdentity: true);
            if (user.id > 0)
            {
                var user_role = new User_Role
                {
                    role_id = 2,
                    user_id = user.id
                };
                await db.InsertAsync(user_role, selectIdentity: true);
            }
            return true;
        }
        public async Task<bool> Update(UpdateUser user)
        {
            using var db = _connectionData.OpenDbConnection();
            var update = await db.SingleByIdAsync<User>(user.id);
            if (update == null) { return false; }
            update.last_name = user.last_name;
            update.family_name = user.family_name;
            update.gender = user.gender;
            update.password = user.password.Length == 32 ? update.password : MD5Services.ComputeMd5Hash(user.password).ToLower();
            update.status = user.status;
            await db.UpdateAsync(update);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return false; }
            return await db.DeleteByIdAsync<User>(id) > 0 ? true : false;
        }

        public async Task<DataTableResult> List(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<User>();
            var predicate = PredicateBuilder.True<User>();
            if (!string.IsNullOrEmpty(page.keyword))
            {
                predicate = predicate.And(e => e.email.ToLower().Contains(page.keyword.ToLower()) || e.family_name.ToLower().Contains(page.keyword.ToLower()) || e.phone.Contains(page.keyword) || e.last_name.ToLower().Contains(page.keyword.ToLower()));
            }
            var totalRecords = await db.CountAsync(predicate);
            if (page.limit > 0) { query.Take(page.limit); }
            if (page.offset > 0) { query.Skip(page.offset); }
            var data = await db.SelectAsync(query);
            return new DataTableResult
            {
                recordsTotal = (int)totalRecords,
                recordsFiltered = (int)totalRecords,
                data = data
            };
        }

        public async Task<User> GetById(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return null!; }
            return await db.SingleByIdAsync<User>(id);
        }
        public User Check_Login(string email_phone, string password)

        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.Single<User>(e => (e.email == email_phone || e.phone == email_phone) && e.password == MD5Services.ComputeMd5Hash(password) && e.status == "active");
            if (query == null) { return null!; }
            return query;
        }

        public async Task<List<User>> GetAll(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<User>();
            query.OrderByDescending(x => x.id);
            if (page.limit > 0) { query.Take(page.limit); }
            if (page.offset > 0) { query.Skip(page.offset); }
            var rows = await db.SelectAsync(query);
            return rows;
        }
        public List<string> GetRolesUser(int user_id)
        {
            using var db = _connectionData.OpenDbConnection();
            var roles = db.Select<User_Role>(ur => ur.user_id == user_id).ToList();
            if (roles != null)
            {
                var roleIds = roles.Select(ur => ur.role_id).ToList();
                var roleTitles = db.Select<Role>(r => roleIds.Contains(r.id));
                return roleTitles.Select(r => r.title).ToList();
            }
            return [];
        }


        public User Valid_Login(string username, string password, out string tokenString)
        {
            tokenString = string.Empty;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) { return null!; }
            var user = Check_Login(username, password);
            if (user != null)
            {
                var roles = GetRolesUser(user.id);
                if (roles != null && roles.Count > 0)
                {
                    List<Claim> claims =
                    [
                        new(ClaimTypes.NameIdentifier, user.id.ToString()),
                        new(ClaimTypes.Name, user.email)
                    ];

                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    // Create token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    string jwt_key = GetAppSetting("Jwt:Key")!;
                    string jwt_issuer = GetAppSetting("Jwt:Issuer")!;
                    string jwt_audience = GetAppSetting("Jwt:Audience")!;
                    var jwt_expires = Convert.ToInt32(GetAppSetting("Jwt:Expires")!);
                    var key_bytes = Encoding.UTF8.GetBytes(jwt_key);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = userIdentity,
                        Expires = DateTime.UtcNow.AddMinutes(jwt_expires),
                        Audience = jwt_audience,
                        Issuer = jwt_issuer,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key_bytes), SecurityAlgorithms.HmacSha512Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    tokenString = tokenHandler.WriteToken(token);

                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt_issuer,
                        ValidAudience = jwt_audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key_bytes)
                    };
                    ClaimsPrincipal claimsPrincipal;
                    claimsPrincipal = tokenHandler.ValidateToken(tokenString, tokenValidationParameters, out SecurityToken validatedToken);

                    // Save user_id to Session
                    _httpContextAccessor.HttpContext!.Session.SetInt32("user_id", user.id);
                    _httpContextAccessor.HttpContext.Session.SetString("token", tokenString);
                    _httpContextAccessor.HttpContext.Session.SetString("role_title", roles[0]);

                    _httpContextAccessor.HttpContext!.Response.Cookies.Append("token", tokenString, new CookieOptions
                    {
                        HttpOnly = false,
                        Secure = false, // Only enable Secure in production
                        SameSite = SameSiteMode.Strict,
                        Expires = !string.IsNullOrEmpty(GetAppSetting("JWT:Expires")) ? DateTime.Now.AddMinutes(Convert.ToInt32(GetAppSetting("JWT:Expires")!)) : DateTime.Now.AddMinutes(60)
                    });
                }
            }
            return user!;
        }

        public bool Logout()
        {
            _httpContextAccessor.HttpContext!.Session.Clear();
            if (_httpContextAccessor.HttpContext!.Session.Keys.Count() > 0) { return false; }
            return true;
        }
    }
}
