using BulletinBoard.Infrastructure.ComponentRegistrar;
using Microsoft.Extensions.DependencyInjection;
using BulletinBoard.Infrastructure.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Сервисы, репозитории, мапперы
builder.Services.RegisterAppServices();
builder.Services.RegisterAppRepositories();
builder.Services.RegistrarAppMappers();

// Контекст Entity framework
builder.Services.RegistrarAppContexsts(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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

app.Run();
