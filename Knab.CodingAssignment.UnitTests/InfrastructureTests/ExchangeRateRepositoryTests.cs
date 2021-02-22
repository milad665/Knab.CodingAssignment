using System.Threading.Tasks;
using Knab.CodingAssignment.Domain.Exchange;
using Knab.CodingAssignment.Infrastructure.ExchangeRateRepository;
using Knab.CodingAssignment.UnitTests.Mocks;
using Knab.CodingAssignment.UnitTests.Mocks.MemoryCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Knab.CodingAssignment.UnitTests.InfrastructureTests
{
    [TestClass]
    public class ExchangeRateRepositoryTests
    {
        [TestMethod]
        public async Task GetExchangeRates_CacheHit_ReturnsCachedList()
        {
            var cachedRates = new ExchangeRates(1, 1.1, 1.2, 1.3, 1.4);
            var mockHttpClientWrapper = MockIHttpClientWrapper.BadRequestMock();
            var mockCacheHit = MockMemoryCache.CacheHitMock(cachedRates);

            var provider = new UsdExchangeRateRepository(mockCacheHit, mockHttpClientWrapper.Object);
            var result = await provider.GetExchangeRates();

            Assert.AreEqual(cachedRates.Usd, result.Usd);
            Assert.AreEqual(cachedRates.Eur, result.Eur);
            Assert.AreEqual(cachedRates.Brl, result.Brl);
            Assert.AreEqual(cachedRates.Gbp, result.Gbp);
            Assert.AreEqual(cachedRates.Aud, result.Aud);
        }

        [TestMethod]
        public async Task GetExchangeRates_CacheMiss_ReturnsApiPrices()
        {
            var apiRates = new ExchangeRates(1, 1.1, 1.2, 1.3, 1.4);
            var mockHttpClientWrapper = MockIHttpClientWrapper.ExchangeRatesApiOkMock(apiRates);
            var mockCacheHit = MockMemoryCache.CacheMissMock();

            var provider = new UsdExchangeRateRepository(mockCacheHit, mockHttpClientWrapper.Object);
            var result = await provider.GetExchangeRates();

            Assert.AreEqual(apiRates.Usd, result.Usd);
            Assert.AreEqual(apiRates.Eur, result.Eur);
            Assert.AreEqual(apiRates.Brl, result.Brl);
            Assert.AreEqual(apiRates.Gbp, result.Gbp);
            Assert.AreEqual(apiRates.Aud, result.Aud);
        }
    }
}