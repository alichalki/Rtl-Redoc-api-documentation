using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiDocumentationUsingRedocAndSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost(Name = "CreateUser")]
        [SwaggerOperation(
                 Summary = "ایجاد کاربر جدید",
                 Description = "با این سرویس می توان کاربر جدید ایجاد کرد , توجه داشته باشید که نام کاربر باید با حروف انگلیسی ثبت شود.",
                 OperationId = "Get",
                 Tags = new[] { "مدیریت کاربران" })]
        [SwaggerResponse(200, "کاربر با موفقیت ایجاد شد", typeof(string))]
        [SwaggerResponse(400, "ولیدیشن فیلد ها اشتباه است", typeof(string))]
        public IActionResult Create(UserAccount inputdata)
        {

            SampleData.Users.Add(inputdata);
            return Ok();
        }



        [HttpGet(Name = "GetUser")]
        [SwaggerOperation(
                 Summary = "گزارش کاربران موجود",
                 Description = "با این سرویس میتوانید از وضعیت کاربران موجود یاخبر شوید.",
                 OperationId = "Get",
                 Tags = new[] { "مدیریت کاربران" })]
        [SwaggerResponse(200, "لیست ماربر های موجود", typeof(UserAccount))]
        [SwaggerResponse(425, "کاربری با این مشخصات یافت نشد", typeof(string))]
        public IActionResult Get(string firstname)
        {
            var res = SampleData.Users.FirstOrDefault(e => e.Firstname.Equals(firstname));
            if (res == null)
                return Ok("کاربری با این مشخصات یافت نشد");

            return Ok(res);
        }



        [HttpDelete(Name = "DeleteUser")]
        [SwaggerOperation(
                 Summary = "حذف کاربر",
                 Description = "با این سرویس میتوانید از طریق نام کاربری اقدام به حذف کاربر کنید.",
                 OperationId = "Get",
                 Tags = new[] { "مدیریت کاربران" })]
        [SwaggerResponse(200, "حذف ماربر های موجود", typeof(string))]
        [SwaggerResponse(425, "کاربری با این مشخصات یافت نشد", typeof(string))]
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
