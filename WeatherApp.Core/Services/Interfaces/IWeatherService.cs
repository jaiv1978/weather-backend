using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core.Models.DTO;

namespace WeatherApp.Core.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponseDto?> GetWeatherInfoByCoordinates(double latitude, double longitude);
        Task<ForecastResponseDto?> GetForecastByPostalAddress(string street, string city, string state, string zipCode);
    }
}
