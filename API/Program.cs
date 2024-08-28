using Microsoft.EntityFrameworkCore;
using JokeAPI.Data.DatabaseContext;
using JokeAPI.Services;
using JokeAPI.Repositories;
using JokeAPI.Interfaces;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

var connectionString = $"Host=roundhouse.proxy.rlwy.net;Port=17016;Database=railway;Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;

// Add services to the container.
builder.Services.AddControllers();

// Register custom services
builder.Services.AddScoped<IJokeRepository, JokeRepository>();
builder.Services.AddScoped<JokeService>(); // Use AddScoped instead of AddSingleton

// Register DbContext
builder.Services.AddDbContext<DatabaseContextClass>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.MapControllers();

app.Run();
