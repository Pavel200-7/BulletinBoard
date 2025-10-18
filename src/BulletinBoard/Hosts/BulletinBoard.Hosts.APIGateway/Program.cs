using BulletinBoard.AppServices.Contexts.Apigateway.Services;
using BulletinBoard.AppServices.Contexts.Apigateway.Services.IServices;
using BulletinBoard.Hosts.APIGateway.Handlers;
using BulletinBoard.Infrastructure.ComponentRegistrar;
using BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.Images;
using BulletinBoard.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Обработкик для автоматической отправки jwt.
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<TokenForwardingHandler>();


// пути HttpClient
builder.Services.AddHttpClient("BulletinService", client =>
{
    client.BaseAddress = new Uri("http://bulletin_service:8080");
})
.AddHttpMessageHandler<TokenForwardingHandler>();

builder.Services.AddHttpClient("ImageService", client =>
{
    client.BaseAddress = new Uri("http://images_service:8080");
})
.AddHttpMessageHandler<TokenForwardingHandler>();



builder.Services.AddHttpClient("UserService", client =>
{
    client.BaseAddress = new Uri("http://user_service:8080");
})
.AddHttpMessageHandler<TokenForwardingHandler>();



// Сервисы, кеш и кеш сервисы
builder.Services.RegisterAPIGatewayServices();
builder.Services.RegistrarAPIGatewayMappers();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IInmenoryImagesIdHolderServise, InmenoryImagesIdHolderServise>();

// настройки для красивого вывода json.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Подключает Swagger с документацией
builder.Services.AddSwaggerWithXmlComments();

builder.Services.AddSwaggerGen(c =>
{
    // Настройка схемы авторизации JWT Bearer
    c.AddSecurityDefinition("Bearer", new()
    {
        Description = "JWT Authorization header using the Bearer scheme. \n\nEnter 'Bearer' [space] and then your token.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new(){
            {
                new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                Array.Empty<string>()
            }
        });
});

var app = builder.Build();


app.UseMiddleware<ExceptionHandlingMiddleware>();

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


await app.RunAsync();

public partial class Program
{ }

