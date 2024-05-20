using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandServices _services;
        public BrandController(IBrandServices services)
        {
            _services = services;
        }
        [HttpPost("/Brand/List")]
        public async Task<ActionResult> List()
        {
            var draw = Request.Form["draw"];
            var start = Request.Form["start"];
            var length = Request.Form["length"];
            string? search = Request.Form["search[value]"];
            var pBrand = new PagingModels
            {
                limit = int.Parse(length!),
                offset = int.Parse(start!),
                keyword = search,
            };
            var result = await _services.List(pBrand);
            return Ok(new
            {
                draw,
                result.recordsTotal,
                result.recordsFiltered,
                result.data
            });
        }
        [HttpPost("/Brand/Create")]
        public ActionResult Create([FromBody] Brand brand)
        {
            if (brand == null) { return NoContent(); }
            return Ok(_services.Create(brand));
        }
        [HttpPut("/Brand/Update")]
        public ActionResult Update([FromBody] Brand brand)
        {
            if (brand == null) { return NoContent(); }
            return Ok(_services.Update(brand));
        }
        [HttpDelete("/Brand/Delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_services.Delete(id));
        }
        [HttpGet]
        [Route("/Brand/Get_All")]
        public ActionResult GetAll(int offset = 0, int limit = 10, string search = "")
        {
            PagingModels pBrand = new() { limit = limit, offset = offset, keyword = search };
            var data = _services.GetAll(pBrand);
            return Ok(data);
        }

        [HttpGet("/Brand/GetById/{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_services.GetById(id));
        }
    }
}
