using WeatherApp.Core.Services.Interfaces;
using System.Net.Http.Headers;

namespace WeatherApp.Core.Services
{
    public class ExternalAPICallService : IExternalAPICallService
    {
        public async Task<string> GetAsync(string url, string userAgent = "")
        {
            using var httpClient = new HttpClient();
            if (!string.IsNullOrWhiteSpace(userAgent))
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            }
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
