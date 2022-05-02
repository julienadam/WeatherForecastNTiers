using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecastNTiers.Common
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        [NotMapped]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
        public string? City { get; set; }
    }
}