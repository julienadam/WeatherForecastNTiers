using System;
using Microsoft.EntityFrameworkCore;
using WeatherForecastNTiers.Common;
using WeatherForecastNTiers.DataAccess;
using Xunit;

namespace WeatherForecastNTiers.UnitTests.DataAccess;

public class WeatherRepositoryTests
{
    [Fact]
    public void Can_retrieve_data_using_city_name()
    {
        var options =
            new DbContextOptionsBuilder<WeatherDbContext>()
                .UseInMemoryDatabase(databaseName: "WeatherDb")
                .Options;
        var context = new WeatherDbContext(options);
        var repo = new DbWeatherRepository(context);

        var entity = new WeatherForecast
        {
            City = "Foo",
            Date = new DateTime(2001, 02, 03),
            TemperatureC = 42,
            Summary = "Beau fixe"
        };

        context.WeatherForecasts.Add(entity);
        context.SaveChanges();

        var returned = repo.GetForecastForCity("Foo");
        Assert.NotNull(returned);
        Assert.Equal(entity, returned);
    }
}