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
            }
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

        options.RoutePrefix = "docs";
        options.DocumentTitle = "سند تست";
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
