
#region AddServices

using WeatherApp.Core.Models;
using WeatherApp.Core.Services;
using WeatherApp.Core.Services.Interfaces;

var corsOrigins = "myCORSOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000");
                      });
});

builder.Services.Configure<ExternalAPIConfig>(builder.Configuration.GetSection(nameof(ExternalAPIConfig)));
builder.Services.AddScoped<IExternalAPICallService, ExternalAPICallService>();
builder.Services.AddScoped<IGeocodingService, GeocodingService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

#endregion

#region Configure 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
