using Data.Entities;
using Data.Models;
using Data.ViewModels.Product;

namespace Bll.Services.Interface
{
    public interface IProductServices
    {
        public bool Create(CreateProductModels product);
        public bool Update(Product product);
        public bool Delete(int id);
        public bool ExcelImport(string filePath);
        public bool UploadImage(string filePath);
        public List<Product> GetAll(PagingModels page);
        public Task<DataTableResult> List(PagingModels page);
        public Product GetById(int id);
    }
}
