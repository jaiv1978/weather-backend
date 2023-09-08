using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core.Models
{
    public class ExternalAPIConfig
    {
        public string GeocodingUrlBase { get; set; }
        public string WeatherUrlBase { get; set; }
    }
}
