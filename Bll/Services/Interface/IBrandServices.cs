using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
    public interface IBrandServices
    {
        public Task<bool> Create(Brand brand);
        public Task<bool> Update(Brand brand);
        public Task<bool> Delete(int id);
        public Task<List<Brand>> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Task<Brand> GetById(int id);
    }
}
