using Bll.Extentions;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using ServiceStack.OrmLite;

namespace Bll.Services.Impliment
{
    public class BrandServices : DbServices, IBrandServices
    {
        public BrandServices()
        {
        }

        public async Task<bool> Create(Brand brand)
        {
            using var db = _connectionData.OpenDbConnection();
            return await db.InsertAsync(brand, selectIdentity: true) > 0 ? true : false;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return false; }
            return await db.DeleteByIdAsync<Brand>(id) > 0 ? true : false;
        }

        public async Task<bool> Update(Brand brand)
        {
            using var db = _connectionData.OpenDbConnection();
            var update = await db.SingleByIdAsync<Brand>(brand.id);
            if (update == null) { return false; }
            await db.UpdateAsync(brand);
            return true;
        }
        public  async Task<List<Brand>> GetAll(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Brand>();
            query.OrderByDescending(x => x.id);
            if (page.limit > 0) { query.Take(page.limit); }
            if (page.offset > 0) { query.Skip(page.offset); }
            var rows = await db.SelectAsync(query);
            return rows;
        }

        public async Task<Brand> GetById(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return null!; }
            return await db.SingleByIdAsync<Brand>(id);
        }

        public async Task<DataTableResult> List(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Brand>();
            var predicate = PredicateBuilder.True<Brand>();
            if (!string.IsNullOrEmpty(page.keyword))
            {
                predicate = predicate.And(e => e.title.ToLower().Contains(page.keyword.ToLower()));
            }
            var totalRecords = await db.CountAsync(predicate);
            if (page.limit > 0) { query.Take(page.limit); }
            if (page.offset > 0) { query.Skip(page.offset); }
            var data = await db.SelectAsync(query);
            return new DataTableResult
            {
                recordsTotal = (int)totalRecords,
                recordsFiltered = (int)totalRecords,
                data = data
            };
        }
    }
}
