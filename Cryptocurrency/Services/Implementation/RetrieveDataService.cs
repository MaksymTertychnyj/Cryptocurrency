using Cryptocurrency.Model.Data;
using Cryptocurrency.Model.Data.Response;
using Cryptocurrency.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cryptocurrency.Services.Implementation
{
    public class RetrieveDataService : IRetrieveDataService
    {
        private readonly HttpClient _httpClient;
        private readonly int limit;

        public RetrieveDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            limit = 100;
        }

        public async Task<Asset> GetAssetByIdAsync(string id)
        {
            var result = await RetrieveDataAsync<ResponseEntity<Asset>>("assets/" + id);
            return result != null ? result.Data : null!;
        }

        public async Task<IEnumerable<Asset>> GetAssetsAsync(int page)
        {
            var param = GetParamString(page);
            var result = await RetrieveDataAsync<ResponseEntities<Asset>>("assets" + param);
            return result != null ? result.Data : null!;
        }

        public async Task<Exchange> GetExchangeByIdAsync(string exchangeId)
        {
            var result = await RetrieveDataAsync<ResponseEntity<Exchange>>("exchanges/" + exchangeId);
            return result != null ? result.Data : null!;
        }

        public async Task<IEnumerable<Exchange>> GetExchangesAsync(int page)
        {
            var param = GetParamString(page);
            var result = await RetrieveDataAsync<ResponseEntities<Exchange>>("exchanges" + param);
            return result != null ? result.Data : null!;
        }

        public async Task<IEnumerable<Market>> GetMarketsAsync(string? baseId, int page)
        {
            var param = GetParamString(page);
            var result = await RetrieveDataAsync<ResponseEntities<Market>>("markets" + param + "&baseId=" + baseId);
            return result != null ? result.Data : null!;
        }

        public async Task<Rate> GetRateAsync(string id)
        {
            var result = await RetrieveDataAsync<ResponseEntity<Rate>>("rates/" + id);
            return result != null ? result.Data : null!;
        }

        public async Task<IEnumerable<Rate>> GetRatesAsync()
        {
            var result = await RetrieveDataAsync<ResponseEntities<Rate>>("rates/");
            return result != null ? result.Data : null!;
        }

        private async Task<TResult> RetrieveDataAsync<TResult>(string request)
        {
            try
            {
                var response = await _httpClient.GetAsync(_httpClient.BaseAddress + request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(content)!;
            }
            catch (Exception)
            {
                return default!;
            }
        }

        private string GetParamString(int page)
        {
            return $"?limit={limit}&offset={page * limit - limit}";
        }
    }
}
