using Microsoft.EntityFrameworkCore;
using WeatherForecastNTiers.Common;

namespace WeatherForecastNTiers.DataAccess;

public class WeatherDbContext : DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}