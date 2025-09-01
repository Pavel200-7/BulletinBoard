using BulletinBoard.Infrastructure.ComponentRegistrar;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;


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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
