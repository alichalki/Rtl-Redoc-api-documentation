using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDocumentationUsingRedocAndSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost(Name = "CreateUser")]
        public IActionResult Create(UserAccount inputdata)
        {
            return Ok();
        }


        [HttpGet(Name = "GetUser")]
        public IActionResult Get(UserAccount inputdata)
        {
            return Ok(inputdata);
        }
    }
}
