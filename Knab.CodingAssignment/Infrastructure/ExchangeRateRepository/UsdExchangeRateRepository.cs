using System;
using System.Text.Json;
using System.Threading.Tasks;
using Knab.CodingAssignment.Domain.Exchange;
using Microsoft.Extensions.Caching.Memory;

namespace Knab.CodingAssignment.Infrastructure.ExchangeRateRepository
{
    public class UsdExchangeRateRepository : IExchangeRateRepository
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public UsdExchangeRateRepository(IMemoryCache cache, IHttpClientWrapper httpClientWrapper)
        {
            _cache = cache;
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<ExchangeRates> GetExchangeRates()
        {
            const string cacheKey = "ExchangeRates";

            if (!_cache.TryGetValue(cacheKey, out ExchangeRates rates))
            {
                rates = await GetFromExternalProvider();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                _cache.Set(cacheKey, rates, cacheEntryOptions);
            }

            return rates;
        }

        private async Task<ExchangeRates> GetFromExternalProvider()
        {
            const string url = "https://api.exchangeratesapi.io/latest?base=USD";
            var response = await _httpClientWrapper.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
                return ExchangeRates.Empty;

            var responseText = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<RatesDto>(responseText);
            return new ExchangeRates(1,result.rates.EUR, result.rates.BRL, result.rates.GBP, result.rates.AUD);

        }
    }
}