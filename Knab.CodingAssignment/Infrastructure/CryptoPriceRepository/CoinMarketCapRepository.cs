using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Knab.CodingAssignment.Domain.Crypto;
using Knab.CodingAssignment.Framework.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace Knab.CodingAssignment.Infrastructure.CryptoPriceRepository
{
    public class CoinMarketCapRepository : ICryptoPriceRepository
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public CoinMarketCapRepository(IMemoryCache cache, IHttpClientWrapper httpClientWrapper)
        {
            _cache = cache;
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<double> GetCryptoPriceInUsd(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new GeneralApplicationException("Invalid Symbol.");

            symbol = symbol.ToUpper();
            var cacheKey = $"CryptoRate-{symbol}";

            if (!_cache.TryGetValue(cacheKey, out double rate))
            {

                rate = await GetFromExternalProvider(symbol);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, rate, cacheEntryOptions);
            }

            return rate;
        }

        private async Task<double> GetFromExternalProvider(string symbol)
        {
            var url = $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?convert=USD&symbol={symbol}";
            _httpClientWrapper.AddHeader("X-CMC_PRO_API_KEY", "4e9ee9a3-77db-4f00-8711-a0d9553a86c1");
            var response = await _httpClientWrapper.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    throw new GeneralApplicationException("Invalid Symbol.");

                return 0;
            }

            var responseText = await response.Content.ReadAsStringAsync();
            var price = Regex.Match(responseText, "\"price\":[0-9]+(\\.[0-9]*)?")?.Value;
            if (string.IsNullOrWhiteSpace(price))
                return 0;

            return double.Parse(price.Split(":")[1].Trim());

        }
    }
}