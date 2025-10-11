using BulletinBoard.Infrastructure.ComponentRegistrar;
using BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.Images;
using BulletinBoard.Infrastructure.Middlewares;


var builder = WebApplication.CreateBuilder(args);


// Контекст Entity framework
var environment = builder.Environment.EnvironmentName;
builder.Services.RegistrarImagesContexsts(builder.Configuration, environment);


// Сервисы, репозитории, мапперы
builder.Services.RegisterImagesServices();
builder.Services.RegisterImagesRepositories();
builder.Services.RegistrarImagesMappers();


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

