using Bll.Extentions;
using Bll.Extentions.Ultilities;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Data.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using ServiceStack.OrmLite;

namespace Bll.Services.Impliment
{
    public class ProductServices : DbServices, IProductServices
    {
        private readonly IConfiguration _app_settings;
        protected readonly string _path_assets;
        public ProductServices(IConfiguration appSettings)
        {
            string default_path_assets = "C:\\data";
            _path_assets = _app_settings!["PathAssets"] ?? default_path_assets;
            _app_settings = appSettings;
        }
        public string GetAppSetting(string key) => _app_settings.GetSection(key).Value!;
        public async Task<bool> ExcelImport(IFormFile file)
        {
            if (file == null || file.Length == 0) { return false; }

            var products = new List<Product>();
            var errorRows = new List<int>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using var package = new ExcelPackage(stream);
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        var product = new Product
                        {
                            code = worksheet.Cells[row, 1].Text,
                            title = worksheet.Cells[row, 2].Text,
                            description = worksheet.Cells[row, 3].Text,
                            price = float.Parse(worksheet.Cells[row, 4].Text),
                            amount = int.Parse(worksheet.Cells[row, 5].Text),
                            image_1 = worksheet.Cells[row, 6].Text,
                            image_2 = worksheet.Cells[row, 7].Text,
                            image_3 = worksheet.Cells[row, 8].Text,
                            image_4 = worksheet.Cells[row, 9].Text,
                            source = worksheet.Cells[row, 10].Text,
                            gender = worksheet.Cells[row, 11].Text,
                            age = worksheet.Cells[row, 12].Text,
                        };
                        products.Add(product);
                    }
                    catch (Exception)
                    {
                        errorRows.Add(row);
                    }
                }
                try
                {
                    using var db = _connectionData.OpenDbConnection();
                    await db.SaveAllAsync(products);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new BadRequestObjectResult("Chưa nhận được dữ liệu của file");

            var allowedTypes = new[] { "image/jpeg", "image/png", "video/mp4", "video/mpeg" };
            if (!allowedTypes.Contains(file.ContentType))
                return new BadRequestObjectResult("Sai định dạng file");

            string url = _app_settings["Url"]!;
            string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_") + StringHelpers.RemoveVietnameseChar(file.FileName, true);
            string urlName = url + fileName;
            var fullPath = Path.Combine(_path_assets, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(stream);

            return new OkObjectResult(new { file = fullPath, url = urlName });
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
