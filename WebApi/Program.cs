using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// DbContext'in ba�lanaca�� veritaban� ba�lant� dizesini al�n
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext'i servis olarak ekleyin
builder.Services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString));

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
