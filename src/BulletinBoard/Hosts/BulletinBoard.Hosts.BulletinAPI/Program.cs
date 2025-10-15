using BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.Bulletin;
using BulletinBoard.Infrastructure.Middlewares;


var builder = WebApplication.CreateBuilder(args);


// �������� Entity framework
builder.Services.RegistrarBulletinContexsts(builder.Configuration);

// �������, �����������, �������
builder.Services.RegisterBulletinServices();
builder.Services.RegisterBulletinRepositories();
builder.Services.RegistrarBulletinMappers();
builder.Services.RegistrarBulletinInitializers();


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


await app.RunAsync();
//await app.InitAndRunAsync();

public partial class Program
{ }

