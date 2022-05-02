using System;
using WeatherForecastNTiers.Common;
using WeatherForecastNTiers.DataAccess;

namespace WeatherForecastNTiers.UnitTests.Business
{
    public class TestRepository : IWeatherRepository
    {
        public WeatherForecast GetForecastForCity(string city)
        {
            if (city == "Vannes")
            {
                return new WeatherForecast
                {
                    City = city, Date = new DateTime(2001, 02, 03), Id = 42, Summary = "Foo", TemperatureC = 23
                };
            }
            else
            {
                return null;
            }
        }
    }
}
