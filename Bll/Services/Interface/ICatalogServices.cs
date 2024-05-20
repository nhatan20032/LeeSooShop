using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
    public interface ICatalogServices
    {
        public bool Create(Catalog catalog);
        public bool Update(Catalog catalog);
        public bool Delete(int id);
        public List<Catalog> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Catalog GetById(int id);
    }
}
