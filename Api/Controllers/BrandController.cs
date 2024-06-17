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
    public class BrandController : ControllerBase
    {
        private readonly IBrandServices _services;
        public BrandController(IBrandServices services)
        {
            _services = services;
        }
        [HttpPost("/Brand/Create")]
        public async Task<ActionResult> Create([FromBody] Brand brand)
        {
            if (brand == null) { return NoContent(); }
            return Ok(await _services.Create(brand));
        }
        [HttpPut("/Brand/Update")]
        public async Task<ActionResult> Update([FromBody] Brand brand)
        {
            if (brand == null) { return NoContent(); }
            return Ok(await _services.Update(brand));
        }
        [HttpDelete("/Brand/Delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(await _services.Delete(id));
        }
        [HttpGet]
        [Route("/Brand/Get_All")]
        public async Task<ActionResult> GetAll(int offset = 0, int limit = 10, string search = "")
        {
            PagingModels pBrand = new() { limit = limit, offset = offset, keyword = search };
            var data = await _services.GetAll(pBrand);
            return Ok(data);
        }

        [HttpGet("/Brand/GetById/{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(await _services.GetById(id));
        }
    }
}
