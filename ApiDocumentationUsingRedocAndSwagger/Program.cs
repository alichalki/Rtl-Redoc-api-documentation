using ApiDocumentationUsingRedocAndSwagger;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            //This will instruct a developer reading about the API on who to contact in case of issues or questions.
            Title = "ChalkiTestApi",
            Description = "<h1 class='redoc_table_title'>فهرست کدهای وضعیت</h1>" +
            "<p class='redoc_table_description'>چالکی تست از <a href=\'https://en.wikipedia.org/wiki/List_of_HTTP_status_codes\'>کدهای وضعیت HTTP</a> برای نشان دادن موفق یا ناموفق بودن درخواست&zwnj;های شما استفاده می&zwnj;کند.\r\nدر صورت ناموفق بودن درخواست، چالکی تست با استفاده از کد وضعیت مناسب، خطایی برمی&zwnj;گرداند.</p>" +
            "<table class='redoc_table'>\r\n<thead>\r\n<tr>\r\n<th>کد وضعیت</th>\r\n<th>توضیحات</th>\r\n</tr>\r\n</thead>\r\n<tbody><tr>\r\n<td><code>2xx</code></td>\r\n<td>عملیات با موفقیت انجام شد.</td>\r\n</tr>\r\n<tr>\r\n<td><code>4xx</code></td>\r\n<td>اطلاعات ارسال شده صحیح نیست.</td>\r\n</tr>\r\n<tr>\r\n<td><code>5xx</code></td>\r\n<td>مشکلی در سرور چالکی تست رخ داده است.</td>\r\n</tr>\r\n</tbody></table>" +
            "<h1 class='redoc_table_title'>فهرست کدهای خطا</h1>" +
            "<p>در صورت بروز خطا در فراخوانی وب&zwnj;سرویس، پاسخی مطابق با جدول زیر دریافت خواهید کرد.</p>" +
            "<table class='redoc_table'>\r\n<thead>\r\n<tr>\r\n<th>کد خطا</th>\r\n<th>توضیحات</th>\r\n</tr>\r\n</thead>\r\n<tbody><tr>\r\n<td><code>server_error</code></td>\r\n<td>یک خطای داخلی رخ داده است.</td>\r\n</tr>\r\n<tr>\r\n<td><code>validation_error</code></td>\r\n<td>اطلاعات ارسال شده صحیح نیست.</td>\r\n</tr>\r\n<tr>\r\n<td><code>authentication_error</code></td>\r\n<td>توکن ارسالی نامعتبر است.</td>\r\n</tr>\r\n<tr>\r\n<td><code>authorization_error</code></td>\r\n<td>به آدرس مورد نظر دسترسی ندارید.</td>\r\n</tr>\r\n<tr>\r\n<td><code>not_found</code></td>\r\n<td>آدرس مورد نظر پیدا نشد.</td>\r\n</tr>\r\n<tr>\r\n<td><code>bad_request</code></td>\r\n<td>درخواست نامعتبر است.</td>\r\n</tr>\r\n<tr>\r\n<td><code>method_not_allowed</code></td>\r\n<td>متد درخواست معتبر نیست.</td>\r\n</tr>\r\n</tbody></table>",
            Version = "v1",

            //use custome logo to redoc api
            Extensions = new Dictionary<string, IOpenApiExtension>
            {
              {"x-logo", new OpenApiObject
                {
                   {"url", new OpenApiString("/assets/MyLogo.png")},
                   { "altText", new OpenApiString("api-logo")}
                }
              }
            },
        });


    //I have added two variables for getting the generated Documentation file for the current assembly and setting the path using the base directory.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    ////this telling SwaggerGen() to include the XML comments generated at startup for Swagger.
    options.IncludeXmlComments(xmlPath);

    //enable annotations to make some more documentation for our api-docs
    options.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    //This is the place where I have defined where I would like swagger.json to appear.

    options.SwaggerEndpoint("/swagger/v1/swagger.json",
    "Swagger Demo Documentation v1"));

    app.UseReDoc(options =>
    {
        //config redoc.
        options.ConfigObject.AdditionalItems.Add("testtt", "testtt");
        options.RoutePrefix = "docs";
        options.DocumentTitle = "ChalkiTestDocumentation";
        options.SpecUrl = "/swagger/v1/swagger.json";
        options.ConfigObject.HideDownloadButton = true;
        options.IndexStream = () => typeof(Program).Assembly
                .GetManifestResourceStream("ApiDocumentationUsingRedocAndSwagger.wwwroot.assets.CustomIndex.ReDoc.index.html"); // requires file to be added as an embedded resource


        options.InjectStylesheet("/css/CustomCss.ReDoc.css");
    });
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
