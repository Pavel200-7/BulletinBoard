using BulletinBoard.Infrastructure.ComponentRegistrar;
using BulletinBoard.Infrastructure.Middlewares;



var builder = WebApplication.CreateBuilder(args);


// �������� Entity framework
var environment = builder.Environment.EnvironmentName;
builder.Services.RegistrarAppContexsts(builder.Configuration, environment);


// �������, �����������, �������
builder.Services.RegisterAppServices();
builder.Services.RegisterAppRepositories();
builder.Services.RegistrarAppMappers();
builder.Services.RegistrarAppInitializers();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// ���������� Swagger � �������������
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


//await app.RunAsync();
await app.InitAndRunAsync();

public partial class Program
{ }

