using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;
using WebApi.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//SqlContext
builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

builder.Services.AddControllers()
	.AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
