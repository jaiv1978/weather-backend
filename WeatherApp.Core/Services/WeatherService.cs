using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core.Models;
using WeatherApp.Core.Models.DTO;
using WeatherApp.Core.Services.Interfaces;

namespace WeatherApp.Core.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IOptions<ExternalAPIConfig> _externalAPIConfig;
        private readonly IExternalAPICallService _externalAPICallService;
        private readonly IGeocodingService _geocodingService;
        private string userAgent = "myweatherapp-jorge-ibarra.com";

        public WeatherService(IOptions<ExternalAPIConfig> externalAPIConfig, IExternalAPICallService externalAPICallService, IGeocodingService geocodingService)
        {
            _externalAPIConfig = externalAPIConfig;
            _externalAPICallService = externalAPICallService;
            _geocodingService = geocodingService;   
        }

        public virtual async Task<WeatherResponseDto?> GetWeatherInfoByCoordinates(double latitude, double longitude)
        {
            var content = await _externalAPICallService.GetAsync($"{_externalAPIConfig.Value.WeatherUrlBase}/points/{latitude},{longitude}", userAgent);
            return JsonConvert.DeserializeObject<WeatherResponseDto>(content);
        }

        public async Task<ForecastResponseDto?> GetForecastByPostalAddress(string street, string city, string state, string zipCode)
        {
            var geocodingResponse = await _geocodingService.GetGeoInfoByPostalAddress(street, city, state, zipCode);
            if (geocodingResponse is null || !geocodingResponse.Result.AddressMatches.Any())
            {
                throw new NullReferenceException("Postal address didn't return valid coordinates");
            }

            var coordinates = geocodingResponse.Result.AddressMatches[0].Coordinates;
            var weatherInfo = await GetWeatherInfoByCoordinates(coordinates.Y, coordinates.X);
            if (weatherInfo is null)
            {
                throw new NullReferenceException("Weather API call returned null instead of weather info.");
            }

            var forecastContent = await _externalAPICallService.GetAsync(weatherInfo.Properties.Forecast, userAgent);
            return JsonConvert.DeserializeObject<ForecastResponseDto>(forecastContent);
        }
    }
}
