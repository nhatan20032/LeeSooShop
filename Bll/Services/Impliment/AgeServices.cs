﻿using Bll.Extentions;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using ServiceStack.OrmLite;

namespace Bll.Services.Impliment
{
    public class AgeServices : DbServices, IAgeServices
    {
        public AgeServices()
        {
        }

        public bool Create(Age age)
        {
            using var db = _connectionData.OpenDbConnection();
            return db.Insert(age, selectIdentity: true) > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return false; }
            return db.DeleteById<Age>(id) > 0 ? true : false;
        }

        public bool Update(Age age)
        {
            using var db = _connectionData.OpenDbConnection();
            var update = db.SingleById<Age>(age.id);
            if (update == null) { return false; }
            db.Update(age);
            return true;
        }
        public List<Age> GetAll(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Age>();
            query.OrderByDescending(x => x.id);
            if (page.limit > 0) { query.Take(page.limit); }
            if (page.offset > 0) { query.Skip(page.offset); }
            var rows = db.Select(query).ToList();
            return rows;
        }

        public Age GetById(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return null!; }
            return db.SingleById<Age>(id);
        }

        public async Task<DataTableResult> List(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Age>();
            var predicate = PredicateBuilder.True<Age>();
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