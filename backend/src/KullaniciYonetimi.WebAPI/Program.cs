using System.Text.Json.Serialization;
using KullaniciYonetimi.Application;
using KullaniciYonetimi.Infrastructure;
using KullaniciYonetimi.WebAPI.Modules;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// secrets.json dosyasını yükle
builder.Configuration.AddJsonFile("config/secrets.json", optional: true, reloadOnChange: true);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173") // frontend port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors();

var app = builder.Build();

app.UseHttpsRedirection();

app.RegisterRoutes();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod()
    .SetIsOriginAllowed(t => true));

app.MapGet("/", () => Results.Redirect("/scalar/v1"));
app.MapOpenApi();
app.MapScalarApiReference();

app.Run();