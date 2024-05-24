using Bll.Extentions;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Data.ViewModels.Product;
using ServiceStack.OrmLite;

namespace Bll.Services.Impliment
{
    public class ProductServices : DbServices, IProductServices
    {
        public ProductServices()
        {

        }

        public bool ExcelImport(string filePath)
        {
            throw new NotImplementedException();
        }

        public bool UploadImage(string filePath)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Create(CreateProductModels product)
        {
            using var db = _connectionData.OpenDbConnection();
            var brandName = db.Single<Brand>(x => x.id == product.brand_id).title;
            var obj_product = new Product
            {
                code = GenerateCodeItem.CodeInput(brandName),
                title = product.title,
                description = product.description,
                price = product.price,
                amount = product.amount,
                image_1 = product.image_1,
                image_2 = product.image_2,
                image_3 = product.image_3,
                image_4 = product.image_4,
                source = product.source,
                gender = product.gender,
                age = product.age,
                created_at = product.created_at,
                status = product.status ?? "active"
            };
            var product_id = (int)await db.InsertAsync(obj_product, selectIdentity: true);
            await db.InsertAsync(new Product_Catalog { product_id = product_id, catalog_id = product.catalog_id }, selectIdentity: true);
            await db.InsertAsync(new Product_Age { product_id = product_id, age_id = product.age_id }, selectIdentity: true);
            await db.InsertAsync(new Product_Brand { product_id = product_id, brand_id = product.brand_id }, selectIdentity: true);
            await db.InsertAsync(new Product_Gender { product_id = product_id, gender_id = product.gender_id }, selectIdentity: true);
            if (product.discount_id > 0) { await db.InsertAsync(new Product_Discount { product_id = product_id, discount_id = product.discount_id }, selectIdentity: true); }
            return true;
        }
        public async Task<bool> Update(Product product)
        {
            using var db = _connectionData.OpenDbConnection();
            var update = db.SingleById<Product>(product.id);
            if (update == null) { return false; }
            update.code = product.code;
            update.title = product.title;
            update.description = product.description;
            update.price = product.price;
            update.amount = product.amount;
            update.image_1 = product.image_1;
            update.image_2 = product.image_2;
            update.image_3 = product.image_3;
            update.image_4 = product.image_4;
            update.source = product.source;
            update.gender = product.gender;
            update.age = product.age;
            update.modified_at = DateTime.Now;
            update.status = product.status;
            await db.UpdateAsync(update);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return false; }
            return await db.DeleteByIdAsync<User>(id) > 0 ? true : false;
        }

        public async Task<List<Product>> GetAll(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Product>();
            query.OrderByDescending(x => x.id);
            if (page.limit > 0) { query.Take(page.limit); }
            if (page.offset > 0) { query.Skip(page.offset); }
            var rows = await db.SelectAsync(query);
            return rows;
        }

        public async Task<Product> GetById(int id)
        {
            using var db = _connectionData.OpenDbConnection();
            if (id <= 0) { return null!; }
            return await db.SingleByIdAsync<Product>(id);
        }

        public async Task<DataTableResult> List(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Product>();
            var predicate = PredicateBuilder.True<Product>();
            if (!string.IsNullOrEmpty(page.keyword))
            {
                predicate = predicate.And(e => e.title.ToLower().Contains(page.keyword.ToLower()) || e.age.ToLower().Contains(page.keyword.ToLower()) || e.gender.Contains(page.keyword));
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
