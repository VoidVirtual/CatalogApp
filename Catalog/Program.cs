using Microsoft.EntityFrameworkCore;
using Catalog.Repositories;

var builder = WebApplication.CreateBuilder(args);
string connection = "Data source = CatalogDB.sqlite";
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connection));
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
