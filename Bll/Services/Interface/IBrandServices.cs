using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
    public interface IBrandServices
    {
        public bool Create(Brand brand);
        public bool Update(Brand brand);
        public bool Delete(int id);
        public List<Brand> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Brand GetById(int id);
    }
}
