using DataAccess.Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Implementaciones;
using Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TrabajadoresPruebaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql"));
});

builder.Services.AddScoped<IRepositoryBase<Trabajador>,TrabajadorRepository>();
builder.Services.AddScoped<IRepositoryBase<Distrito>, DistritoRepository>();
builder.Services.AddScoped<IRepositoryBase<Provincia>, ProvinciaRepository>();
builder.Services.AddScoped<IRepositoryBase<Departamento>, DepartamentoRepository>();

// Add services to the container.

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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
