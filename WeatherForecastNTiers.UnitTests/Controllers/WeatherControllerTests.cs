using System;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastNTiers.Business;
using WeatherForecastNTiers.Common;
using WeatherForecastNTiers.Controllers;
using Xunit;

namespace WeatherForecastNTiers.UnitTests.Controllers
{
    public class WeatherControllerTests
    {
        [Fact]
        public void Bad_request_if_city_is_not_set()
        {
            IWeatherForecastService service = new NopeWeatherService();
            var controller = new WeatherForecastController(service, null);
            var result = controller.GetByCity(null);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Code_404_if_no_data_for_city()
        {
            IWeatherForecastService service = new NopeWeatherService();
            var controller = new WeatherForecastController(service, null);
            var result = controller.GetByCity("Foo");
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Weather_data_if_data_present_for_city()
        {
            IWeatherForecastService service = new StaticWeatherService();
            var controller = new WeatherForecastController(service, null);
            var result = controller.GetByCity("Foo");
            var okObject = Assert.IsType<OkObjectResult>(result);
            var weatherResult = Assert.IsType<WeatherForecast>(okObject.Value);
            Assert.Equal(42, weatherResult.Id);
        }
    }

    public class NopeWeatherService : IWeatherForecastService
    {
        public WeatherForecast GetForeCast(string city, string country)
        {
            return null;
        }
    }

    public class StaticWeatherService : IWeatherForecastService
    {
        public WeatherForecast GetForeCast(string city, string country)
        {
            return new WeatherForecast
            {
                City = city, Id = 42, Date = new DateTime(2001, 02, 03), TemperatureC = 42, Summary = "Foo"
            };
        }
    }
}
