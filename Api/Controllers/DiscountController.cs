using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
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
                result.recordsFiltered,
                result.data
            });
        }
        [HttpPost("/Discount/Create")]
        public ActionResult Create([FromBody] Discount discount)
        {
            if (discount == null) { return NoContent(); }
            return Ok(_services.Create(discount));
        }
        [HttpPut("/Discount/Update")]
        public ActionResult Update([FromBody] Discount discount)
        {
            if (discount == null) { return NoContent(); }
            return Ok(_services.Update(discount));
        }
        [HttpDelete("/Discount/Delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_services.Delete(id));
        }
        [HttpGet]
        [Route("/Discount/Get_All")]
        public ActionResult GetAll(int offset = 0, int limit = 10, string search = "")
        {
            PagingModels pDiscount = new() { limit = limit, offset = offset, keyword = search };
            var data = _services.GetAll(pDiscount);
            return Ok(data);
        }

        [HttpGet("/Discount/GetById/{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_services.GetById(id));
        }
    }
}
