using System;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastNTiers.Business;
using WeatherForecastNTiers.Common;
using WeatherForecastNTiers.Controllers;
using WeatherForecastNTiers.ViewModels;
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
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Code_404_if_no_data_for_city()
        {
            IWeatherForecastService service = new NopeWeatherService();
            var controller = new WeatherForecastController(service, null);
            var result = controller.GetByCity("Foo");
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Weather_data_if_data_present_for_city()
        {
            IWeatherForecastService service = new StaticWeatherService();
            var controller = new WeatherForecastController(service, null);
            var result = controller.GetByCity("Foo");
            var okObject = Assert.IsType<OkObjectResult>(result.Result);
            var weatherResult = Assert.IsType<WeatherForecastViewModel>(okObject.Value);
            Assert.Equal("Foo", weatherResult.Location);
            Assert.Equal("Lorem ipsum dolor et sic rosa lupus...", weatherResult.Summary);
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
                City = city,
                Id = 42,
                Date = new DateTime(2001, 02, 03),
                TemperatureC = 42,
                Summary = "Lorem ipsum dolor et sic rosa lupus"
            };
        }
    }
}
