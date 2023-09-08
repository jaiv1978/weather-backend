using Microsoft.AspNetCore.Mvc;
using WeatherApp.Core.Services.Interfaces;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/v1/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGeocodingService _geocodingService; 
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGeocodingService geocodingService, IWeatherService weatherService)
        {
            _logger = logger;
            _geocodingService = geocodingService;
            _weatherService = weatherService;
        }
            
        [HttpGet("forecast")]
        public async Task<ActionResult> GetForecast(string street, string city = "", string state= "", string zipCode = "")
        {
            try
            {
                var forecasts = await _weatherService.GetForecastByPostalAddress(street, city, state, zipCode);
                if (forecasts is null)
                {
                    return BadRequest("Weather API call returned null instead of forecast info.");
                }

                return Ok(forecasts.Properties.Periods);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}