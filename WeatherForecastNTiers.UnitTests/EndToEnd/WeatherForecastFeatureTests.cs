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
        private WeatherDbContext context;

        public WeatherForecastFeatureTests()
        {
            var options =
                new DbContextOptionsBuilder<WeatherDbContext>()
                    .UseInMemoryDatabase(databaseName: "WeatherDb")
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

        [Fact]
        public void Weather_data_if_data_present_for_city()
        {
            var repository = new DbWeatherRepository(context);
            var service = new WeatherForecastService(repository);
            var controller = new WeatherForecastController(service, null);

            var result = controller.GetByCity("Foo");
            var okObject = Assert.IsType<OkObjectResult>(result);
            var weatherResult = Assert.IsType<WeatherForecast>(okObject.Value);
            Assert.Equal("Beau fixe", weatherResult.Summary);
            Assert.Equal("Foo", weatherResult.City);
            Assert.Equal(42, weatherResult.TemperatureC);
        }
    }
}
