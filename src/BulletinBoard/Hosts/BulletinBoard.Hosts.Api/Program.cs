using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.ComponentRegistrar;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using BulletinBoard.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);


// Контекст Entity framework
builder.Services.RegistrarAppContexsts(builder.Configuration);


// Сервисы, репозитории, мапперы
builder.Services.RegisterAppServices();
builder.Services.RegisterAppRepositories();
builder.Services.RegistrarAppMappers();
builder.Services.RegistrarAppInitializers();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

await app.InitAndRunAsync();
