using Data.Entities;
using Data.Models;
using Data.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bll.Services.Interface
{
    public interface IProductServices
    {
        public Task<bool> Create(CreateProductModels product);
        public Task<bool> Update(Product product);
        public Task<bool> Delete(int id);
        public Task<bool> ExcelImport(IFormFile file);
        public Task<ActionResult> UploadFile(IFormFile file);
        public Task<List<Product>> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Task<Product> GetById(int id);
    }
}
