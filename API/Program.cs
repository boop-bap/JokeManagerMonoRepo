using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DotNetEnv;

using JokeAPI.Data.DatabaseContext;
using JokeAPI.Services;
using JokeAPI.Repositories;
using JokeAPI.Interfaces;
using JokeAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

var connectionString = $"Host=roundhouse.proxy.rlwy.net;Port=17016;Database=railway;Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;

// Add services to the container.
builder.Services.AddControllers();


// Register services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJokeService, JokeService>();

// Register DbContext
builder.Services.AddDbContext<DatabaseContextClass>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContextClass>()
    .AddDefaultTokenProviders();

// Configure CORS policy if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
