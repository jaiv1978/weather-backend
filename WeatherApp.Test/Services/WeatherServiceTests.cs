using NSubstitute;
using WeatherApp.Core.Services.Interfaces;
using Microsoft.Extensions.Options;
using WeatherApp.Core.Models;
using WeatherApp.Core.Services;
using WeatherApp.Core.Models.DTO;
using NSubstitute.Core.Arguments;
using AutoFixture;

namespace WeatherApp.Test.Services
{
    public class WeatherServiceTests
    {
        private readonly IOptions<ExternalAPIConfig> _externalAPIConfig;
        private readonly IExternalAPICallService _externalAPICallService;
        private readonly IGeocodingService _geocodingService;
        private readonly IWeatherService _weatherService;
        private readonly Fixture _fixture;
        private readonly string _street;
        private readonly string _city;
        private readonly string _state;
        private readonly string _forecastUrl;

        public WeatherServiceTests() 
        {
            _externalAPICallService = Substitute.For<IExternalAPICallService>();
            _geocodingService = Substitute.For<IGeocodingService>();
            _externalAPIConfig = Substitute.For<IOptions<ExternalAPIConfig>>();
            _weatherService = Substitute.For<WeatherService>(_externalAPIConfig, _externalAPICallService, _geocodingService);
            _fixture = new Fixture();
            _street = "4600 Silver Hill Rd";
            _city = "Washington";
            _state = "DC";
            _forecastUrl = "https://forecast-url";
    }

        [Fact]
        public async Task GetForecastByPostalAddress_ValidPostalAddress_ReturnValidForecastInfo()
        {
            var weatherResponseDto = _fixture.Create<WeatherResponseDto>();
            weatherResponseDto.Properties.Forecast = _forecastUrl;

            var geocodingResponseDto = _fixture.Create<GeocodingResponseDto>();

            var latitude = geocodingResponseDto.Result.AddressMatches[0].Coordinates.Y;
            var longitude = geocodingResponseDto.Result.AddressMatches[0].Coordinates.X;
            _geocodingService.GetGeoInfoByPostalAddress(_street, _city, _state, "").Returns(geocodingResponseDto);
            _weatherService.GetWeatherInfoByCoordinates(latitude, longitude).Returns(weatherResponseDto);
            _externalAPICallService.GetAsync(_forecastUrl, Arg.Any<string>()).Returns(Constants.forecastStrResponse);

            var forecasts = await _weatherService.GetForecastByPostalAddress(_street, _city, _state, "");
            Assert.True(forecasts!.Properties.Periods.Any());
        }

        [Fact]
        public async Task GetForecastByPostalAddress_InvalidPostalAddress_ThrowException()
        {
            var geocodingResponseDto = _fixture.Create<GeocodingResponseDto>();
            _geocodingService.GetGeoInfoByPostalAddress(_street, _city, _state, "").Returns(geocodingResponseDto);

            var ex = await Record.ExceptionAsync(() => _weatherService.GetForecastByPostalAddress("", _city, _state, ""));
            Assert.IsType<NullReferenceException>(ex);
            Assert.Contains("Postal address didn't return valid coordinates", ex.Message);
        }

        [Fact]
        public async Task GetForecastByPostalAddress_NoWeatherInfo_ThrowException()
        {
            var geocodingResponseDto = _fixture.Create<GeocodingResponseDto>();
            var weatherResponseDto = _fixture.Create<WeatherResponseDto>();

            var latitude = 38.84598652130676;
            var longitude = -76.92743610939091;
            _geocodingService.GetGeoInfoByPostalAddress(_street, _city, _state, "").Returns(geocodingResponseDto);
            _weatherService.GetWeatherInfoByCoordinates(latitude, longitude).Returns(weatherResponseDto);

            var ex = await Record.ExceptionAsync(() => _weatherService.GetForecastByPostalAddress(_street, _city, _state, ""));
            Assert.IsType<NullReferenceException>(ex);
            Assert.Contains("Weather API call returned null instead of weather info.", ex.Message);
        }
    }
}
