namespace WeatherForecastNTiers.ViewModels
{
    public class WeatherForecastViewModel
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
        public string? Location { get; set; }
    }
}
