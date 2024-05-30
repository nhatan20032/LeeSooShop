using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
    public interface IAgeServices
    {
        public Task<bool> Create(Age age);
        public Task<bool> Update(Age age);
        public Task<bool> Delete(int id);
        public Task<List<Age>> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Task<Age> GetById(int id);
    }
}
