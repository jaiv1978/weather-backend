using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core.Models.DTO
{
    public class WeatherResponseDto
    {
        public WeatherPropertiesDto Properties { get; set; }
    }

    public class WeatherPropertiesDto
    {
        public string Forecast { get; set; }
        public RelativeLocationDto RelativeLocation { get; set; }
    }

    public class RelativeLocationDto
    {
        public RelativeLocationPropertiesDto Properties { get; set; }
    }

    public class RelativeLocationPropertiesDto
    {
        public string City { get; set; }
        public string State { get; set; }
    }
}
