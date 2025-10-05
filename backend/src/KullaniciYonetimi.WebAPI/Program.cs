using System.Text.Json.Serialization;
using KullaniciYonetimi.Application;
using KullaniciYonetimi.Infrastructure;
using KullaniciYonetimi.WebAPI.Modules;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("config/secrets.json", optional: true, reloadOnChange: true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.RegisterRoutes();

app.MapGet("/", () => Results.Redirect("/scalar/v1"));
app.MapOpenApi();
app.MapScalarApiReference();

app.Run();
