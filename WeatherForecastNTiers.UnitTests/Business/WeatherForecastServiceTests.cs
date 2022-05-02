using WeatherForecastNTiers.Business;
using Xunit;

namespace WeatherForecastNTiers.UnitTests.Business;

public class WeatherForecastServiceTests
{
    [Fact]
    public void Null_result_for_countries_other_than_france()
    {
        var repository = new TestRepository();
        var service = new WeatherForecastService(repository);
        var result = service.GetForeCast("", "de");
        Assert.Null(result);
    }

    [Fact]
    public void Correct_result_for_french_city_with_data()
    {
        var repository = new TestRepository();
        var service = new WeatherForecastService(repository);
        var result = service.GetForeCast("Vannes", "fr");
        Assert.NotNull(result);
        Assert.Equal(42, result.Id);
    }

    [Fact]
    public void No_result_for_french_city_without_data()
    {
        var repository = new TestRepository();
        var service = new WeatherForecastService(repository);
        var result = service.GetForeCast("Rennes", "fr");
        Assert.Null(result);
    }
}