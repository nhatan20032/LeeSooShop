﻿using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgeController : ControllerBase
    {
        private readonly IAgeServices _services;
        public AgeController(IAgeServices services)
        {
            _services = services;
        }
        [HttpPost("/Age/List")]
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
        [HttpPost("/Age/Create")]
        public ActionResult Create([FromBody] Age age)
        {
            if (age == null) { return NoContent(); }
            return Ok(_services.Create(age));
        }
        [HttpPut("/Age/Update")]
        public ActionResult Update([FromBody] Age age)
        {
            if (age == null) { return NoContent(); }
            return Ok(_services.Update(age));
        }
        [HttpDelete("/Age/Delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_services.Delete(id));
        }
        [HttpGet]
        [Route("/Age/Get_All")]
        public ActionResult GetAll(int offset = 0, int limit = 10, string search = "")
        {
            PagingModels page = new() { limit = limit, offset = offset, keyword = search };
            var data = _services.GetAll(page);
            return Ok(data);
        }

        [HttpGet("/Age/GetById/{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_services.GetById(id));
        }
    }
}