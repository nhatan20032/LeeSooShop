using Data.Entities;
using Data.Models;
using Data.ViewModels.Product;

namespace Bll.Services.Interface
{
    public interface IProductServices
    {
        public Task<bool> Create(CreateProductModels product);
        public Task<bool> Update(Product product);
        public Task<bool> Delete(int id);
        public Task<bool> ExcelImport(string filePath);
        public Task<bool> UploadImage(string filePath);
        public Task<List<Product>> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Task<Product> GetById(int id);
    }
}
