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

            SampleData.Users.Add(inputdata);
            return Ok();
        }


        [HttpGet(Name = "GetUser")]
        public IActionResult Get(string firstname)
        {
            var res = SampleData.Users.FirstOrDefault(e => e.Firstname.Equals(firstname));
            if (res == null)
                return Ok("کاربری با این مشخصات یافت نشد");

            return Ok(res);
        }

        [HttpDelete(Name = "DeleteUser")]
        public IActionResult Delete(string firstname)
        {
            var res = SampleData.Users.FirstOrDefault(e => e.Firstname.Equals(firstname));
            SampleData.Users.Remove(res);

            if (res == null)
                return Ok("کاربری با این مشخصات یافت نشد");

            return Ok(res);
        }
    }
}
