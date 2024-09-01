using JokeUI.Components;
using JokeUI.Services;
using JokeUI.Configuration;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Register AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


// Register HttpClient
builder.Services.AddHttpClient();

// Register JokeService
builder.Services.AddScoped<JokeService>();

// Register logging
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();