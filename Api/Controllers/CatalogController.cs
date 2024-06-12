using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogServices _services;
        public CatalogController(ICatalogServices services)
        {
            _services = services;
        }
        [HttpPost("/Catalog/Create")]
        public async Task<ActionResult> Create([FromBody] Catalog catalog)
        {
            if (catalog == null) { return NoContent(); }
            return Ok(await _services.Create(catalog));
        }
        [HttpPut("/Catalog/Update")]
        public async Task<ActionResult> Update([FromBody] Catalog catalog)
        {
            if (catalog == null) { return NoContent(); }
            return Ok(await _services.Update(catalog));
        }
        [HttpDelete("/Catalog/Delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(await _services.Delete(id));
        }
        [HttpGet]
        [Route("/Catalog/Get_All")]
        public async Task<ActionResult> GetAll(int offset = 0, int limit = 10, string search = "")
        {
            PagingModels page = new() { limit = limit, offset = offset, keyword = search };
            var data = await _services.GetAll(page);
            return Ok(data);
        }

		[HttpGet]
		[Route("/Catalog/Get_All_Parent_Catalog")]
		public async Task<ActionResult> Get_All_Parent_Catalog()
		{
			var data = await _services.GetAllParentCatalog();
			return Ok(data);
		}

		[HttpGet("/Catalog/GetById/{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(await _services.GetById(id));
        }
    }
}
