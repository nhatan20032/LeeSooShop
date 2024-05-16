using Bll.Extentions;
using Bll.Interface;
using Data.Entities;
using Data.Models;
using Data.ViewModels.User;
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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DataTableResult GetAll(PagingModels paging)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Login(UserLogin user)
        {
            throw new NotImplementedException();
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
