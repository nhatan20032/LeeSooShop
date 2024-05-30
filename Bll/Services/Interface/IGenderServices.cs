using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
    public interface IGenderServices
    {
        public Task<bool> Create(Gender gender);
        public Task<bool> Update(Gender gender);
        public Task<bool> Delete(int id);
        public Task<List<Gender>> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Task<Gender> GetById(int id);
    }
}
