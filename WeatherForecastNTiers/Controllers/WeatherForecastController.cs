using Microsoft.AspNetCore.Mvc;
using WeatherForecastNTiers.Business;
using WeatherForecastNTiers.Common;
using WeatherForecastNTiers.DataAccess;
using WeatherForecastNTiers.ViewModels;

namespace WeatherForecastNTiers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService service;
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(IWeatherForecastService service, ILogger<WeatherForecastController> logger)
        {
            this.service = service;
            this.logger = logger;
        }
        
        [HttpGet]
        [Route(nameof(GetByCity))]
        public ActionResult<WeatherForecastViewModel> GetByCity(string city = "Vannes")
        {
            if (string.IsNullOrEmpty(city))
            {
                // Essayer de trouver la localisation avec l'adresse ip ?
                return BadRequest(new { Status = "Invalid parameters", Message = "City was not provided"});
            }

            var result = service.GetForeCast(city, "fr");
            
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(new WeatherForecastViewModel
            {
                Location = result.City,
                TemperatureC = result.TemperatureC,
                Summary = result.Summary + "..."
            });
        }
    }
}