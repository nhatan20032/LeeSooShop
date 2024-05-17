using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Data.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _services;
        public UserController(IUserServices services)
        {
            _services = services;
        }
        [HttpPost("/User/List")]
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
            var result = await _services.List(page);
            return Ok(new
            {
                draw,
                result.recordsTotal,
                result.recordsFiltered,
                result.data
            });
        }
        [HttpPost("/User/Create")]
        public ActionResult Create([FromBody] User user)
        {
            if (user == null) { return NoContent(); }
            return Ok(_services.Create(user));
        }
        [HttpPut("/User/Update")]
        public ActionResult Update([FromBody] UpdateUser user)
        {
            if (user == null) { return NoContent(); }
            return Ok(_services.Update(user));
        }
        [HttpDelete("/User/Delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_services.Delete(id));
        }
        [HttpGet]
        [Route("/User/Get_All")]
        public ActionResult GetAll(int offset = 0, int limit = 10, string search = "")
        {
            PagingModels page = new() { limit = limit, offset = offset, keyword = search };
            var data = _services.GetAll(page);
            return Ok(data);
        }
        [HttpGet("/User/GetById/{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_services.GetById(id));
        }
    }
}
