using Bll.Services.Interface;
using Data.Models;
using Data.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;
        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }
        [HttpGet("/Cart/Get_All_Product_In_User_Cart")]
        public async Task<ActionResult> GetAllProductInUserCart(int offset = 0, int limit = 10, string search = "")
        {
            PagingModels page = new() { limit = limit, offset = offset, keyword = search };
            var data = await _cartServices.GetAllProductInUserCart(page);
            return Ok(data);
        }
        [HttpPost("/Cart/List")]
        public async Task<ActionResult> List()
        {
            var draw = Request.Form["draw"];
            var start = Request.Form["start"];
            var length = Request.Form["length"];
            string? search = Request.Form["search[value]"];
            var page = new PagingModels
            {
                limit = int.Parse(length!),
                offset = int.Parse(start!),
                keyword = search,
            };
            var result = await _cartServices.List(page);
            return Ok(new
            {
                draw,
                result.recordsTotal,

                result.data
            });
        }
        [HttpPost("/Cart/Add_Product_Into_User_Cart")]
        public async Task<ActionResult> AddProductIntoUserCart([FromBody] ProductIntoUserCart user_c)
        {
            if (user_c == null) { return NoContent(); }
            return Ok(await _cartServices.AddProductIntoUserCart(user_c));
        }
        [HttpPost("/Cart/Check_Quantity")]
        public async Task<ActionResult> CheckProductInStored(int quantity)
        {
            if (quantity <= 0) { return NoContent(); }
            return Ok(await _cartServices.CheckProductInStored(quantity));
        }
        [HttpDelete("/Cart/Remove_One_Product")]
        public async Task<ActionResult> RemoveOneProductInUserCart(int product_id, int user_id)
        {
            if (product_id <= 0 || user_id <= 0) { return NoContent(); }
            return Ok(await _cartServices.RemoveOneProductInUserCart(product_id, user_id));
        }
        [HttpDelete("/Cart/Remove_All_Product")]
        public async Task<ActionResult> RemoveAllProductInUserCart(List<int> product_ids, int user_id)
        {
            if (product_ids.Count() <= 0 || user_id <= 0) { return NoContent(); }
            return Ok(await _cartServices.RemoveAllProductInUserCart(product_ids, user_id));
        }
    }
}
