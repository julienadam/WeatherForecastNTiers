using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherForecastNTiers.Business;
using WeatherForecastNTiers.Common;
using WeatherForecastNTiers.Controllers;
using WeatherForecastNTiers.DataAccess;
using Xunit;

namespace WeatherForecastNTiers.UnitTests.EndToEnd
{
    public class WeatherForecastFeatureTests
    {
        private readonly WeatherForecastController controller;
        private static readonly WeatherDbContext context;

        static WeatherForecastFeatureTests()
        {
            var options =
                new DbContextOptionsBuilder<WeatherDbContext>()
                    .UseInMemoryDatabase(databaseName: "WeatherForecastFeatureTestsDb")
                    .Options;
            context = new WeatherDbContext(options);
            var entity = new WeatherForecast
            {
                City = "Foo",
                Date = new DateTime(2001, 02, 03),
                TemperatureC = 42,
                Summary = "Beau fixe"
            };

            context.WeatherForecasts.Add(entity);
            context.SaveChanges();
        }

        public WeatherForecastFeatureTests()
        {
            var repository = new DbWeatherRepository(context);
            var service = new WeatherForecastService(repository);
            controller = new WeatherForecastController(service, null);
        }

        [Fact]
        public void Weather_data_if_data_present_for_city()
        {
            var result = controller.GetByCity("Foo");
            var okObject = Assert.IsType<OkObjectResult>(result);
            var weatherResult = Assert.IsType<WeatherForecast>(okObject.Value);
            Assert.Equal("Beau fixe", weatherResult.Summary);
            Assert.Equal("Foo", weatherResult.City);
            Assert.Equal(42, weatherResult.TemperatureC);
        }

        [Fact]
        public void Weather_data_is_not_found_if_data_not_present_for_city()
        {
            var result = controller.GetByCity("dhjgfjhdg");
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
