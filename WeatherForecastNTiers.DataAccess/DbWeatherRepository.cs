using WeatherForecastNTiers.Common;

namespace WeatherForecastNTiers.DataAccess;

public class DbWeatherRepository : IWeatherRepository
{
    private readonly WeatherDbContext context;

    public DbWeatherRepository(WeatherDbContext context)    
    {
        this.context = context;
    }

    public WeatherForecast? GetForecastForCity(string city)
    {
        return context.WeatherForecasts.FirstOrDefault(f => f.City == city);
    }
}