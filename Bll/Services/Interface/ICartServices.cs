using Data.Entities;
using Data.Models;
using Data.ViewModels.Cart;

namespace Bll.Services.Interface
{
    public interface ICartServices
    {
        public Task<DataTableResult> List(PagingModels page);
        public Task<bool> CheckProductInStored(int quantity);
        public Task<List<v_Cart>> GetAllProductInUserCart(PagingModels page);
        public Task<Product_Cart> AddProductIntoUserCart(ProductIntoUserCart user_c);
        public Task<bool> RemoveOneProductInUserCart(int product_id, int user_id);
        public Task<bool> RemoveAllProductInUserCart(List<int> product_ids, int user_id);
    }
}
