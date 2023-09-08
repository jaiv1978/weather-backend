using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Core.Models.DTO;

namespace WeatherApp.Core.Services.Interfaces
{
    public interface IGeocodingService
    {
        Task<GeocodingResponseDto?> GetGeoInfoByPostalAddress(string street, string city, string state, string zipcode);
    }
}
