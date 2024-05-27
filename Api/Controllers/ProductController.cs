using Bll.Services.Interface;
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
        [HttpPost]
        [Route("/Content/Upload_File")]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            return await _productServices.UploadFile(file);
        }
    }
}
