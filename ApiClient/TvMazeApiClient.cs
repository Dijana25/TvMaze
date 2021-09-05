using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TvMaze.ApiClient
{
    public class TvMazeApiClient : ITvMazeApiClient
    {            
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;

        public TvMazeApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(Constants.BASE_URL);
        }

        public Task<List<ShowInfo>> GetShowsAsync(int page)
        {
            return GetJsonAsync<List<ShowInfo>>($"/shows?page={page}");
        }

        public Task<List<Person>> GetShowCastAsync(int showId)
        {
            return GetJsonAsync<List<Person>>($"/shows/{showId}/cast");
        }

        private async Task<T> GetJsonAsync<T>(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error:" + response.StatusCode);
            }

            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
