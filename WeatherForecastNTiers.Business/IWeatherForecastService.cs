using WeatherForecastNTiers.Common;

namespace WeatherForecastNTiers.Business;

public interface IWeatherForecastService
{
    WeatherForecast? GetForeCast(string city, string country);
}