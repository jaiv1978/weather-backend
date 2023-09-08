using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core.Models.DTO
{
    public class ForecastResponseDto
    {
        public ForecastPropertiesDto Properties { get; set; }
    }

    public class ForecastPropertiesDto
    {
        public List<ForecastPeriodDto> Periods { get; set; }
    }

    public class ForecastPeriodDto
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Icon { get; set; }
        public string ShortForecast { get; set; }
        public string DetailedForecast { get; set; }
    }
}
