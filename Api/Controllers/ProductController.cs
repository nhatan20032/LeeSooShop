using Bll.Services.Interface;
using Data.Entities;
using Data.Models;
using Data.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpPost("/Product/List")]
        [Authorize(Roles = "Admin")]
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
            var result = await _productServices.List(page);
            return Ok(new
            {
                draw,
                result.recordsTotal,
                result.recordsFiltered,
                result.data
            });
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Product/Upload_File")]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            return await _productServices.UploadFile(file);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Product/Import_Excel")]
        public async Task<IActionResult> ExcelImport(IFormFile file)
        {
            if (file == null || file.Length == 0) { return BadRequest("File không hợp lệ"); }
            var result = await _productServices.ExcelImport(file);
            return result ? Ok("Thêm sản phẩm từ File Excel thành công") : StatusCode(500, "Đã xảy ra lỗi khi thêm sản phẩm từ Excel");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Product/Create")]
        public async Task<ActionResult> Create([FromBody] CreateProductModels models)
        {
            if (models == null) { return NoContent(); }
            return Ok(await _productServices.Create(models));
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("/Product/Update")]
        public async Task<ActionResult> Update([FromBody] Product product)
        {
            if (product == null) { return NoContent(); }
            return Ok(await _productServices.Update(product));
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("/Product/Delete")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(await _productServices.Delete(id));
        }
        [HttpGet]
        [Route("/Product/Get_All")]
        public ActionResult GetAll(int offset = 0, int limit = 10, string search = "")
        {
            PagingModels page = new() { limit = limit, offset = offset, keyword = search };
            var data = _productServices.GetAll(page);
            return Ok(data);
        }

        [HttpGet("/Product/GetById/{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            if (id <= 0) { return NoContent(); }
            return Ok(_productServices.GetById(id));
        }
    }
}
