using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
    public interface IGenderServices
    {
        public bool Create(Gender gender);
        public bool Update(Gender gender);
        public bool Delete(int id);
        public List<Gender> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Gender GetById(int id);
    }
}
