using System.Globalization;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;
using Core.Extensions;
using Core.DependencyResolver;

var builder = WebApplication.CreateBuilder(args);

// 1) Uygulamanın dinleyeceği URL ve portu belirleyelim (HTTP için 5119)
builder.WebHost.UseUrls("http://localhost:5119");

// Controller'ları uygulamaya kaydet

builder.Services.AddControllers();



var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });


// Autofac için Service Provider Factory ekleniyor
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Autofac Container yapılandırması
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
    containerBuilder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
    containerBuilder.RegisterModule(new Business.DependencyResolvers.AutoFac.AutoFacBusinsessModule());
});




// 2) Swashbuckle ile Swagger servislerini ekleyelim
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyResolvers(new ICoreModule[]{
    new CoreModule()
});

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();


// 3) Swagger middleware’lerini ekleyelim
app.UseSwagger();
app.UseSwaggerUI();

// 4) Test aşamasında HTTPS yönlendirmesi yapmak istemiyorsak kapatalım
//    (Eğer HTTPS sertifikanız düzgün ayarlıysa kullanmaya devam edebilirsiniz)
app.UseHttpsRedirection();

// 5) WeatherForecast endpoint’i
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool",
    "Mild", "Warm", "Balmy", "Hot",
    "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast");





app.MapControllers();
app.Run();

// 6) WeatherForecast kayıt tipi
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    // Fahrenheit hesabını basitçe property ile yapalım
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

}