using Bll.Extentions;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using ServiceStack.OrmLite;

namespace Bll.Services.Impliment
{
    public class GenderServices : DbServices, IGenderServices
    {
        public GenderServices()
        {            
        }

        public bool Create(Gender gender)
        {
            using var db = _connectionData.OpenDbConnection();
            return db.Insert(gender, selectIdentity: true) > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return false; }
            return db.DeleteById<Age>(id) > 0 ? true : false;
        }

        public bool Update(Gender gender)
        {
            using var db = _connectionData.OpenDbConnection();
            var update = db.SingleById<Gender>(gender.id);
            if (update == null) { return false; }
            db.Update(gender);
            return true;
        }

        public List<Gender> GetAll(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Gender>();
            query.OrderByDescending(x => x.id);
            if (page.limit > 0) { query.Take(page.limit); }
            if (page.offset > 0) { query.Skip(page.offset); }
            var rows = db.Select(query).ToList();
            return rows;
        }

        public Gender GetById(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return null!; }
            return db.SingleById<Gender>(id);
        }

        public async Task<DataTableResult> List(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Gender>();
            var predicate = PredicateBuilder.True<Gender>();
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
