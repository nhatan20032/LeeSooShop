using Bll.Extentions;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using ServiceStack.OrmLite;

namespace Bll.Services.Impliment
{
    public class CatalogServices : DbServices, ICatalogServices
    {
        public async Task<bool> Create(Catalog catalog)
        {
            using var db = _connectionData.OpenDbConnection();
            return await db.InsertAsync(catalog, selectIdentity: true) > 0 ? true : false;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return false; }
            return await db.DeleteByIdAsync<Catalog>(id) > 0 ? true : false;
        }

        public async Task<bool> Update(Catalog catalog)
        {
            using var db = _connectionData.OpenDbConnection();
            var update = await db.SingleByIdAsync<Catalog>(catalog.id);
            if (update == null) { return false; }
            await db.UpdateAsync(catalog);
            return true;
        }

        public async Task<DataTableResult> GetAll(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var predicate = PredicateBuilder.True<v_Catalog>();
            if (!string.IsNullOrEmpty(page.keyword)) { predicate = predicate.And(e => e.catalog_title!.ToLower().Contains(page.keyword.ToLower()) || e.parent_title!.ToLower().Contains(page.keyword.ToLower())); }
            var query = db.From<v_Catalog>().Where(predicate);
            var totalRecords = await db.CountAsync(predicate);
            query.OrderBy(x => x.id);
            if (page.limit >= 0) { query.Take(page.limit); }
            if (page.offset > 0) { query.Skip(page.offset); }
            var data = await db.SelectAsync(query);
            return new DataTableResult
            {
                recordsTotal = (int)totalRecords,
                data = data
            };
        }

        public async Task<DataTableResult> GetAllParentCatalog()
        {
            using var db = _connectionData.OpenDbConnection();
            var predicate = PredicateBuilder.True<v_Catalog>();
            predicate = predicate.And(e => e.parent_id == 0);
            var query = db.From<v_Catalog>().Where(predicate);
            var totalRecords = await db.CountAsync(predicate);
            query.OrderBy(x => x.id);
            var data = await db.SelectAsync(query);
            return new DataTableResult
            {
                recordsTotal = (int)totalRecords,
                data = data
            }; ;
        }

        public async Task<Catalog> GetById(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return null!; }
            return await db.SingleByIdAsync<Catalog>(id);
        }
    }
}
