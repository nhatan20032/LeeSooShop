using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountServices _services;
        public DiscountController(IDiscountServices services)
        {
            _services = services;
        }
        [HttpPost("/Discount/List")]
        public async Task<ActionResult> List()
        {
            var draw = Request.Form["draw"];
            var start = Request.Form["start"];
            var length = Request.Form["length"];
            string? search = Request.Form["search[value]"];
            var pDiscount = new PagingModels
            {
                limit = int.Parse(length!),
                offset = int.Parse(start!),
                keyword = search,
            };
            var result = await _services.List(pDiscount);
            return Ok(new
            {
                draw,
                result.recordsTotal,

                result.data
            });
        }
        [HttpPost("/Discount/Create")]
        public async Task<ActionResult> Create([FromBody] Discount discount)
        {
            if (discount == null) { return NoContent(); }
            return Ok(await _services.Create(discount));
        }
        [HttpPut("/Discount/Update")]
        public async Task<ActionResult> Update([FromBody] Discount discount)
        {
            if (discount == null) { return NoContent(); }
            return Ok(await _services.Update(discount));
        }
        [HttpDelete("/Discount/Delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(await _services.Delete(id));
        }
        [HttpGet]
        [Route("/Discount/Get_All")]
        public async Task<ActionResult> GetAll(int offset = 0, int limit = 10, string search = "")
        {
            PagingModels pDiscount = new() { limit = limit, offset = offset, keyword = search };
            var data = await _services.GetAll(pDiscount);
            return Ok(data);
        }

        [HttpGet("/Discount/GetById/{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(await _services.GetById(id));
        }
    }
}
