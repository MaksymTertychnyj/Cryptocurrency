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
            return (await RetrieveDataAsync<ResponseEntity<Asset>>("assets/" + id)).Data;
        }

        public async Task<IEnumerable<Asset>> GetAssetsAsync(int page)
        {
            var param = GetParamString(page);
            return (await RetrieveDataAsync<ResponseEntities<Asset>>("assets" + param)).Data;
        }

        public async Task<Exchange> GetExchangeByIdAsync(string exchangeId)
        {
            return (await RetrieveDataAsync<ResponseEntity<Exchange>>("exchanges/" + exchangeId)).Data;
        }

        public async Task<IEnumerable<Exchange>> GetExchangesAsync(int page)
        {
            var param = GetParamString(page);
            return (await RetrieveDataAsync<ResponseEntities<Exchange>>("exchanges" + param)).Data;
        }

        public async Task<IEnumerable<Market>> GetMarketsAsync(string? assetId, int page)
        {
            var param = GetParamString(page);
            return (await RetrieveDataAsync<ResponseEntities<Market>>("markets" + param + "&assetId=" + assetId)).Data;
        }

        public async Task<Rate> GetRateAsync(string id)
        {
            return (await RetrieveDataAsync<ResponseEntity<Rate>>("rates/" + id)).Data;
        }

        public async Task<IEnumerable<Rate>> GetRatesAsync()
        {
            return (await RetrieveDataAsync<ResponseEntities<Rate>>("rates/")).Data;   
        }

        private async Task<TResult> RetrieveDataAsync<TResult>(string request)
        {
            try
            {
                var response = await _httpClient.GetAsync(_httpClient.BaseAddress + request);
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
