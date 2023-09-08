using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core.Services.Interfaces
{
    public interface IExternalAPICallService
    {
        Task<string> GetAsync(string url, string userAgent = "");
    }
}
