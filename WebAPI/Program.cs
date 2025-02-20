using System.Globalization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

var builder = WebApplication.CreateBuilder(args);

// 1) Uygulamanın dinleyeceği URL ve portu belirleyelim (HTTP için 5119)
builder.WebHost.UseUrls("http://localhost:5119");

// Controller'ları uygulamaya kaydet

builder.Services.AddControllers();
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();

// Autofac için Service Provider Factory ekleniyor
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Autofac Container yapılandırması
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
    containerBuilder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
});




// 2) Swashbuckle ile Swagger servislerini ekleyelim
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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