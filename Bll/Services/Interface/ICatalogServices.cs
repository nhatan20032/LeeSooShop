using Data.Entities;
using Data.Models;

namespace Bll.Services.Interface
{
    public interface ICatalogServices
    {
        public Task<bool> Create(Catalog catalog);
        public Task<bool> Update(Catalog catalog);
        public Task<bool> Delete(int id);
        public Task<DataTableResult> GetAll(PagingModels page);
        public Task<DataTableResult> GetAllParentCatalog();
		public Task<Catalog> GetById(int id);
    }
}
