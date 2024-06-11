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
	public class BannerController : ControllerBase
	{
		private readonly IBannerServices _services;
        public BannerController(IBannerServices services)
        {
			_services = services;
		}
		[HttpPost("/Banner/List")]
		public async Task<ActionResult> List()
		{
			var draw = Request.Form["draw"];
			var start = Request.Form["start"];
			var length = Request.Form["length"];
			string? search = Request.Form["search[value]"];
			var Banner = new PagingModels
			{
				limit = int.Parse(length!),
				offset = int.Parse(start!),
				keyword = search,
			};
			var result = await _services.List(Banner);
			return Ok(new
			{
				draw,
				result.recordsTotal,
				result.data
			});
		}
		[HttpPost("/Banner/Create")]
		public async Task<ActionResult> Create([FromBody] Banner banner)
		{
			if (banner == null) { return NoContent(); }
			return Ok(await _services.Create(banner));
		}
		[HttpPut("/Banner/Update")]
		public async Task<ActionResult> Update([FromBody] Banner banner)
		{
			if (banner == null) { return NoContent(); }
			return Ok(await _services.Update(banner));
		}
		[HttpDelete("/Banner/Delete/{id}")]
		public async Task<ActionResult> Delete([FromRoute] int id)
		{
			if (id <= 0) { return NoContent(); }
			return Ok(await _services.Delete(id));
		}
		[HttpGet]
		[Route("/Banner/Get_All")]
		public async Task<ActionResult> GetAll(int offset = 0, int limit = 10, string search = "")
		{
			PagingModels page = new() { limit = limit, offset = offset, keyword = search };
			var data = await _services.GetAll(page);
			return Ok(data);
		}

		[HttpGet("/Banner/GetById/{id}")]
		public async Task<ActionResult> GetById([FromRoute] int id)
		{
			if (id <= 0) { return NoContent(); }
			return Ok(await _services.GetById(id));
		}
	}
}
