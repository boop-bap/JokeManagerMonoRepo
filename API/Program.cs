using JokeAPI.Data.DatabaseContext;
using JokeAPI.Configuration;
using JokeAPI.Repositories;
using JokeAPI.Interfaces;
using JokeAPI.Services;
using JokeAPI.Entities;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using DotNetEnv;


var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

var connectionString = $"Host=roundhouse.proxy.rlwy.net;Port=17016;Database=railway;Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

// Add services to the container.
builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<IJokeRepository, JokeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJokeService, JokeService>();

// Register DbContext with logging
builder.Services.AddDbContext<DatabaseContextClass>(options =>
{
    options.UseNpgsql(connectionString);
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

// Load JWT settings from environment variables
var jwtSettings = builder.Configuration.GetSection("Jwt");
var privateKey = Environment.GetEnvironmentVariable("ENV_PRIVATE_KEY");
var publicKey = Environment.GetEnvironmentVariable("ENV_PUBLIC_KEY");

builder.Services.Configure<JwtSettings>(options =>
{
    options.Issuer = jwtSettings["Issuer"];
    options.Audience = jwtSettings["Audience"];
    options.PrivateKey = privateKey;
    options.PublicKey = publicKey;
});

// Add Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContextClass>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();