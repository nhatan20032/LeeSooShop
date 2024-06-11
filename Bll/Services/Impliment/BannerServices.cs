using Bll.Extentions;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using ServiceStack.OrmLite;

namespace Bll.Services.Impliment
{
	public class BannerServices : DbServices, IBannerServices
	{
        public BannerServices()
        {            
        }

        public async Task<bool> Create(Banner banner)
		{
			using var db = _connectionData.OpenDbConnection();
			return await db.InsertAsync(banner, selectIdentity: true) > 0 ? true : false;
		}

		public async Task<bool> Delete(int id)
		{
			using var db = _connectionData.OpenDbConnection();
			if (id <= 0) { return false; }
			return await db.DeleteByIdAsync<Banner>(id) > 0 ? true : false;
		}

		public async Task<bool> Update(Banner banner)
		{
			using var db = _connectionData.OpenDbConnection();
			var update = await db.SingleByIdAsync<Banner>(banner.id);
			if (update == null) { return false; }
			await db.UpdateAsync(banner);
			return true;
		}

		public async Task<List<Banner>> GetAll(PagingModels page)
		{
			using var db = _connectionData.OpenDbConnection();
			var query = db.From<Banner>();
			query.OrderByDescending(x => x.id);
			if (page.limit > 0) { query.Take(page.limit); }
			if (page.offset > 0) { query.Skip(page.offset); }
			var rows = await db.SelectAsync(query);
			return rows;
		}

		public async Task<Banner> GetById(int id)
		{
			using var db = _connectionData.OpenDbConnection();
			if (id <= 0) { return null!; }
			return await db.SingleByIdAsync<Banner>(id);
		}

		public async Task<DataTableResult> List(PagingModels page)
		{
			using var db = _connectionData.OpenDbConnection();
			var query = db.From<Banner>();
			var predicate = PredicateBuilder.True<Banner>();
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
				
				data = data
			};
		}
	}
}
