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
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogServices _services;
        public CatalogController(ICatalogServices services)
        {
            _services = services;
        }
        [HttpPost("/Catalog/List")]
        public async Task<ActionResult> List()
        {
            var draw = Request.Form["draw"];
            var start = Request.Form["start"];
            var length = Request.Form["length"];
            string? search = Request.Form["search[value]"];
            var Catalog = new PagingModels
            {
                limit = int.Parse(length!),
                offset = int.Parse(start!),
                keyword = search,
            };
            var result = await _services.List(Catalog);
            return Ok(new
            {
                draw,
                result.recordsTotal,
                result.recordsFiltered,
                result.data
            });
        }
        [HttpPost("/Catalog/Create")]
        public ActionResult Create([FromBody] Catalog catalog)
        {
            if (catalog == null) { return NoContent(); }
            return Ok(_services.Create(catalog));
        }
        [HttpPut("/Catalog/Update")]
        public ActionResult Update([FromBody] Catalog catalog)
        {
            if (catalog == null) { return NoContent(); }
            return Ok(_services.Update(catalog));
        }
        [HttpDelete("/Catalog/Delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_services.Delete(id));
        }
        [HttpGet]
        [Route("/Catalog/Get_All")]
        public ActionResult GetAll(int offset = 0, int limit = 10, string search = "")
        {
            PagingModels pCatalog = new() { limit = limit, offset = offset, keyword = search };
            var data = _services.GetAll(pCatalog);
            return Ok(data);
        }

        [HttpGet("/Catalog/GetById/{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_services.GetById(id));
        }
    }
}
