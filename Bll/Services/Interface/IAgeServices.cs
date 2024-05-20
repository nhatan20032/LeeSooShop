using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
    public interface IAgeServices
    {
        public bool Create(Age age);
        public bool Update(Age age);
        public bool Delete(int id);
        public List<Age> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Age GetById(int id);
    }
}
