using Contta.Models;
using Contta.Services;
using Contta.Core.Interfaces;
using Contta.Infrastructure.Repositories;
using Contta.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<DespesaService>();

builder.Services.AddDbContext<ConttaDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConttaDb")));

builder.Services.AddScoped<IDespesaRepository, DespesaRepository>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ConttaWeb", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "http://localhost:5174"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("ConttaWeb");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
