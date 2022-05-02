using WeatherForecastNTiers.Common;

namespace WeatherForecastNTiers.DataAccess;

public interface IWeatherRepository
{
    WeatherForecast? GetForecastForCity(string city);
}