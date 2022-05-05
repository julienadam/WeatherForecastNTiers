using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecastNTiers.Common
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }
        public string? City { get; set; }
    }
}