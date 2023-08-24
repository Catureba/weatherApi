using Microsoft.EntityFrameworkCore;
using weatherApi.Data;
using weatherApi.Data.Repository;
using weatherApi.Interfaces;
using weatherApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MeteorologicalContext>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("WeatherConection")));

builder.Services.AddScoped<IMeteorologicalService, MeteorologicalService>();
builder.Services.AddScoped<IMeteorologicalRepository, MeteorologicalRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddSingleton <IWeatherService, WeatherService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
