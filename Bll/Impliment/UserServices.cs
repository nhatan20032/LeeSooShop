using Bll.Extentions;
using Bll.Interface;
using Data.Entities;
using Data.Models;
using Data.ViewModels.User;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceStack.OrmLite;

namespace Bll.Impliment
{
    public class UserServices : DbServices, IUserServices
    {
        public UserServices()
        {
        }

        public bool Create(User user)
        {
            using var db = _connectionData.OpenDbConnection();
            return db.Insert(user, selectIdentity: true) > 0 ? true : false;
        }
        public bool Update(UpdateUser user)
        {
            using var db = _connectionData.OpenDbConnection();
            var update = db.SingleById<User>(user.id);
            if (update == null) { return false; }
            update.last_name = user.last_name;
            update.family_name = user.family_name;
            update.phone = user.phone;
            update.gender = user.gender;
            update.password = user.password;
            update.status = user.status;
            db.Update(update);
            return true;
        }

        public bool Delete(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return false; }
            return db.DeleteById<User>(id) > 0 ? true : false;
        }

        public bool Login(UserLogin user)
        {
            throw new NotImplementedException();
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<DataTableResult> GetAll(PagingModels page)
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

        public User GetById(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return null!; }
            return db.SingleById<User>(id);
        }
    }
}
