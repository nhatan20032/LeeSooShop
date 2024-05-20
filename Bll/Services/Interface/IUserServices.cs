using Data.Entities;
using Data.Models;
using Data.ViewModels.User;

namespace Bll.Services.Interface
{
    public interface IUserServices
    {
        public bool Register(User user);
        public bool Update(UpdateUser user);
        public bool Delete(int id);
        public List<User> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public User GetById(int id);
        public User Check_Login(string email, string password);
        public User Valid_Login(string username, string password, out string tokenString);
        public bool Logout();
    }
}
