using Bll.Extentions;
using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Data.ViewModels.Cart;
using ServiceStack.OrmLite;

namespace Bll.Services.Impliment
{
    public class CartServices : DbServices, ICartServices
    {
        public CartServices()
        {
        }

        public async Task<List<v_Cart>> GetAllProductInUserCart(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<v_Cart>();
            query.OrderByDescending(x => x.id);
            if (page.limit > 0) { query.Take(page.limit); }
            if (page.offset > 0) { query.Skip(page.offset); }
            var rows = await db.SelectAsync(query);
            return rows;
        }

        public async Task<bool> CheckProductInStored(int quantity)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Product>().Where(t => t.amount > 0 && t.amount > quantity);
            bool exists = await db.ExistsAsync(query);
            return exists;
        }

        public async Task<Product_Cart> AddProductIntoUserCart(ProductIntoUserCart user_c)
        {
            using var db = _connectionData.OpenDbConnection();
            bool checking = await CheckProductInStored(user_c.quantity); if (!checking) { return null!; }
            var cart_product = new Product_Cart
            {
                product_id = user_c.product_id,
                cart_id = user_c.cart_id,
                quantity = user_c.quantity
            };
            await db.InsertAsync(cart_product, selectIdentity: true);
            return cart_product;
        }

        public async Task<bool> RemoveAllProductInUserCart(List<int> product_ids, int user_id)
        {
            using var db = _connectionData.OpenDbConnection();
            bool exists = await db.ExistsAsync<Product_Cart>(uc => product_ids.Contains(uc.product_id) && uc.cart_id == user_id);
            if(!exists) { return false; }
            await db.DeleteAsync<Product_Cart>(uc => product_ids.Contains(uc.product_id) && uc.cart_id == user_id);
            return true;
        }

        public async Task<bool> RemoveOneProductInUserCart(int product_id, int user_id)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<Product_Cart>().Where(t => t.product_id == product_id && t.cart_id == user_id);
            var result = await db.SingleAsync(query);
            if (result == null) { return false; }
            await db.DeleteAsync(query);
            return true;
        }
        public async Task<DataTableResult> List(PagingModels page)
        {
            using var db = _connectionData.OpenDbConnection();
            var query = db.From<v_Cart>();
            var predicate = PredicateBuilder.True<v_Cart>();
            if (!string.IsNullOrEmpty(page.keyword))
            {
                predicate = predicate.And(e => e.title!.ToLower().Contains(page.keyword.ToLower()));
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
