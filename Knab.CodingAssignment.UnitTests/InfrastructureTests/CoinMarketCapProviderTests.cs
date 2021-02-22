using System.Threading.Tasks;
using Knab.CodingAssignment.Framework.Exceptions;
using Knab.CodingAssignment.Infrastructure.CryptoPriceRepository;
using Knab.CodingAssignment.UnitTests.Mocks;
using Knab.CodingAssignment.UnitTests.Mocks.MemoryCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Knab.CodingAssignment.UnitTests.InfrastructureTests
{
    [TestClass]
    public class CoinMarketCapProviderTests
    {
        [TestMethod]
        [ExpectedException(typeof(GeneralApplicationException))]
        public async Task GetCryptoPriceInUsd_InvalidSymbol_ThrowsException()
        {
            var mockHttpClientWrapper = MockIHttpClientWrapper.BadRequestMock();
            var mockCacheHit = MockMemoryCache.CacheMissMock();

            var provider = new CoinMarketCapRepository(mockCacheHit, mockHttpClientWrapper.Object);
            await provider.GetCryptoPriceInUsd("NOSYMBOL");
        }

        [TestMethod]
        [ExpectedException(typeof(GeneralApplicationException))]
        public async Task GetCryptoPriceInUsd_NullSymbol_ThrowsException()
        {
            var mockHttpClientWrapper = MockIHttpClientWrapper.BadRequestMock();
            var mockCacheHit = MockMemoryCache.CacheMissMock();

            var provider = new CoinMarketCapRepository(mockCacheHit, mockHttpClientWrapper.Object);
            await provider.GetCryptoPriceInUsd(null);
        }

        [TestMethod]
        public async Task GetCryptoPriceInUsd_CacheHit_ReturnsCachedPrice()
        {
            var cachedValue = 1000.0;
            var mockHttpClientWrapper = MockIHttpClientWrapper.CoinMarketCapOkMock("ETH", 200.0);
            var mockCacheHit = MockMemoryCache.CacheHitMock(cachedValue);

            var provider = new CoinMarketCapRepository(mockCacheHit, mockHttpClientWrapper.Object);
            var result = await provider.GetCryptoPriceInUsd("ETH");

            Assert.AreEqual(cachedValue, result);
        }

        [TestMethod]
        public async Task GetCryptoPriceInUsd_CacheMiss_ReturnsApiPrice()
        {
            var apiValue = 1000.0;
            var mockHttpClientWrapper = MockIHttpClientWrapper.CoinMarketCapOkMock("ETH", apiValue);
            var mockCacheHit = MockMemoryCache.CacheMissMock();

            var provider = new CoinMarketCapRepository(mockCacheHit, mockHttpClientWrapper.Object);
            var result = await provider.GetCryptoPriceInUsd("ETH");

            Assert.AreEqual(apiValue, result);
        }
    }
}
