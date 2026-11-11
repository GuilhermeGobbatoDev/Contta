using Contta.Api.Services;
using Contta.Core.Interfaces;
using Contta.Infrastructure.Data;
using Contta.Infrastructure.Identity;
using Contta.Infrastructure.Repositories;
using Contta.Models;
using Contta.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<DespesaService>();

builder.Services.AddDbContext<ConttaDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConttaDb")));

builder.Services.AddIdentityCore<ApplicationUser>(options => { options.User.RequireUniqueEmail = true; }).AddEntityFrameworkStores<ConttaDbContext>();

builder.Services.AddScoped<IDespesaRepository, DespesaRepository>();

builder.Services.AddScoped<JwtTokenService>();

var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Chave JWT não configurada.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Convert.FromBase64String(jwtKey)),

        ValidateIssuer = true,
        ValidIssuer = "Contta",

        ValidateAudience = true,
        ValidAudience = "Contta.Api",

        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
