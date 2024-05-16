using Bll.Interface;
using Data.Entities;
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

        [HttpPost("/User/Create")]
        public ActionResult Create([FromBody] User user)
        {
            if (user == null) { return NoContent(); }
            return Ok(_services.Create(user));
        }
    }
}
