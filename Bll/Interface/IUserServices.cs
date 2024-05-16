using Data.Entities;
using Data.Models;
using Data.ViewModels.User;

namespace Bll.Interface
{
    public interface IUserServices
    {
        public bool Create(User user);
        public bool Update(User user);
        public bool Delete(int id);
        public DataTableResult GetAll(PagingModels paging);
        public User GetById(int id);
        public bool Login(UserLogin user);
        public bool Logout();
    }
}
