using JokeAPI.Services;
using JokeAPI.Repositories;
using JokeAPI.Interfaces;
using JokeAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetEnv;
using JokeAPI.Data.DatabaseContext;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};Port={Environment.GetEnvironmentVariable("DB_PORT")};Database={Environment.GetEnvironmentVariable("DB_NAME")};Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};SslMode=Require;Trust Server Certificate=true";

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