using BulletinBoard.Domain.Entities.User;
using BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.User;
using BulletinBoard.Infrastructure.DataAccess.Contexts.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var environment = builder.Environment.EnvironmentName;
builder.Services.RegistrarUserContexsts(builder.Configuration, environment);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<UserContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Аунтификация
builder.Services.AddAuthentication();

// Авторизация
builder.Services.AddAuthorization();



var app = builder.Build();

app.MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
