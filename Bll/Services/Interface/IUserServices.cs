using Data.Entities;
using Data.Models;
using Data.ViewModels.User;

namespace Bll.Services.Interface
{
    public interface IUserServices
    {
        public bool Create(User user);
        public bool Update(UpdateUser user);
        public bool Delete(int id);
        public List<User> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels paging);
        public User GetById(int id);
        public bool Check_Login(UserLogin user);
        public User Valid_Login(string username, string password, out string tokenString);
        public bool Logout();
    }
}
