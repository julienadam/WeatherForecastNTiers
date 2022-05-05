using Microsoft.EntityFrameworkCore;
using WeatherForecastNTiers.Business;
using WeatherForecastNTiers.DataAccess;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<WeatherDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IWeatherRepository, DbWeatherRepository>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        
    });
    app.UseSwaggerUI(options =>
    {
        
    });
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Required for hosted end-to-end tests
public partial class Program { }