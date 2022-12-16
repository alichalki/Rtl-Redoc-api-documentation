using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiDocumentationUsingRedocAndSwagger
{
    /// <summary>
    /// User Model
    /// </summary> 
    [SwaggerSchema(Required = new[] { "Firstname", "Lastname", "Email" }, Title = "یوزر مدل")]
    public class UserAccount
    {
        [SwaggerSchema(
            Title = "نام",
            Description = "نام باید یا حروف انگلیسی نوشته شده باشد.",
            Format = "string")]
        public string Firstname { get; set; }
        [SwaggerSchema(
            Title = "نام خانوادگی",
            Description = "نام خانوادگی باید یا حروف انگلیسی نوشته شده باشد.",
            Format = "string")]
        public string Lastname { get; set; }
        [SwaggerSchema(
            Title = "ایمیل",
            Description = "ایمیل باید یا حروف انگلیسی نوشته شده باشد.",
            Format = "string")]
        public string Email { get; set; }
    }
}
