using Bll.Extentions;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceStack.OrmLite;

namespace Bll.Services.Impliment
{
    public class DiscountServices : DbServices, IDiscountServices
    {
        public bool Create(Discount discount)
        {
            using var db = _connectionData.OpenDbConnection();
            return db.Insert(discount, selectIdentity: true) > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return false; }
            return db.DeleteById<Discount>(id) > 0 ? true : false;
        }

        public bool Update(Discount discount)
        {
            using var db = _connectionData.OpenDbConnection();
            var update = db.SingleById<Discount>(discount.id);
            if (update == null) { return false; }
            db.Update(discount);
            return true;
        }

        public List<Discount> GetAll(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Discount>();
            query.OrderByDescending(x => x.id);
            if (page.limit > 0) { query.Take(page.limit); }
            if (page.offset > 0) { query.Skip(page.offset); }
            var rows = db.Select(query).ToList();
            return rows;
        }

        public Discount GetById(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return null!; }
            return db.SingleById<Discount>(id);
        }

        public async Task<DataTableResult> List(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Discount>();
            var predicate = PredicateBuilder.True<Discount>();
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
