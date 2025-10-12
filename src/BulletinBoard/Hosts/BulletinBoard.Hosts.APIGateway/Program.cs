using BulletinBoard.AppServices.Contexts.Apigateway.Services;
using BulletinBoard.AppServices.Contexts.Apigateway.Services.IServices;
using BulletinBoard.Infrastructure.ComponentRegistrar;
using BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.Images;
using BulletinBoard.Infrastructure.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("BulletinService", client =>
{
    client.BaseAddress = new Uri("http://bulletin_service:8080");
});

builder.Services.AddHttpClient("ImageService", client =>
{
    client.BaseAddress = new Uri("http://images_service:8080");
});


// Сервисы, кеш и кеш сервисы
builder.Services.RegisterAPIGatewayServices();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IInmenoryImagesIdHolderServise, InmenoryImagesIdHolderServise>();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Подключает Swagger с документацией
builder.Services.AddSwaggerWithXmlComments();

var app = builder.Build();


app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


await app.RunAsync();

public partial class Program
{ }

