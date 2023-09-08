using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core.Models.DTO
{
    public class GeocodingResponseDto
    {
        public GeocodingResultDto Result { get; set; }
    }

    public class GeocodingResultDto
    {
        public List<GeocodingAddressMatchesDto> AddressMatches { get; set; }
    }

    public class GeocodingAddressMatchesDto
    {
        public CoordinatesDto Coordinates { get; set; }
    }

    public class CoordinatesDto
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
