using Microsoft.EntityFrameworkCore;
using TeamProjectServer.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using TeamProjectServer.Data;
using TeamProjectServer.Services;



var builder = WebApplication.CreateBuilder(args);

//appsetting.json 연결 문자열을 읽어서 AppDbContext 설정(DB 연결설정)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
DataManager.Initialize();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
