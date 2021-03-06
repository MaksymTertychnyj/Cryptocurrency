using Cryptocurrency.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Services.Interfaces
{
    public interface IRetrieveDataService
    {
        Task<IEnumerable<Asset>> GetAssetsAsync(int page);
        Task<Asset> GetAssetByIdAsync(string id);
        Task<IEnumerable<Market>> GetMarketsAsync(string? baseId, int page);
        Task<IEnumerable<Exchange>> GetExchangesAsync(int page);
        Task<Exchange> GetExchangeByIdAsync(string exchangeId);
        Task<IEnumerable<Rate>> GetRatesAsync();
        Task<Rate> GetRateAsync(string id);
    }
}
