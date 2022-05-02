using WeatherForecastNTiers.Common;
using WeatherForecastNTiers.DataAccess;

namespace WeatherForecastNTiers.Business
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherRepository repository;

        public WeatherForecastService(IWeatherRepository repository)
        {
            this.repository = repository;
        }
        
        public WeatherForecast? GetForeCast(string city, string country)
        {
            if (country == "fr")
            {
                return repository.GetForecastForCity(city);
            }
            else
            {
                return null;
            }
        }
    }
}
