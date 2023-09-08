using Microsoft.Extensions.Options;
using WeatherApp.Core.Models;
using WeatherApp.Core.Models.DTO;
using WeatherApp.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace WeatherApp.Core.Services
{
    public class GeocodingService : IGeocodingService
    {
        private readonly IOptions<ExternalAPIConfig> _externalAPIConfig;
        private readonly IExternalAPICallService _externalAPICallService;
        public GeocodingService(IOptions<ExternalAPIConfig> externalAPIConfig, IExternalAPICallService externalAPICallService)
        {
            _externalAPIConfig = externalAPIConfig;
            _externalAPICallService = externalAPICallService;
        }

        public async Task<GeocodingResponseDto?> GetGeoInfoByPostalAddress(string street, string city, string state, string zipcode)
        {
            var content = await _externalAPICallService.GetAsync($"{_externalAPIConfig.Value.GeocodingUrlBase}/geocoder/locations/address?street={street}&city={city}&state={state}&zip={zipcode}&benchmark=Public_AR_Census2020&format=json");
            return JsonConvert.DeserializeObject<GeocodingResponseDto>(content);
        }
    }
}
