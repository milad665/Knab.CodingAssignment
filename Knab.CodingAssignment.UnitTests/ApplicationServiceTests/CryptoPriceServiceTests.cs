using System.Threading.Tasks;
using Knab.CodingAssignment.ApplicationServices;
using Knab.CodingAssignment.Domain.Exchange;
using Knab.CodingAssignment.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Knab.CodingAssignment.UnitTests.ApplicationServiceTests
{
    [TestClass]
    public class CryptoPriceServiceTests
    {
        [TestMethod]
        public async Task GetPrices_CalculatesCorrectly()
        {
            var symbol = "CRYPTO";
            var cryptoPrice = 100.0;
            var exchangeRates = new ExchangeRates(1,1.1,1.2,1.3,1.4);
            var resultPriceList = new CryptoPriceList(
                symbol, 
                cryptoPrice * exchangeRates.Usd, cryptoPrice * exchangeRates.Eur,
                cryptoPrice * exchangeRates.Brl, cryptoPrice * exchangeRates.Gbp, cryptoPrice * exchangeRates.Aud);
            
            var mockCryptoPriceRepo = MockICryptoPriceRepository.ValidCryptoPriceRepository(cryptoPrice);
            var mockExchangeRateRepo = MockIExchangeRateRepository.ValidExchangeRatesRepository(exchangeRates);

            var provider = new CryptoPriceService(mockExchangeRateRepo.Object, mockCryptoPriceRepo.Object);
            var result = await provider.GetPrices(symbol);

            Assert.AreEqual(resultPriceList.Symbol, result.Symbol);
            Assert.AreEqual(resultPriceList.Usd, result.Usd);
            Assert.AreEqual(resultPriceList.Eur, result.Eur);
            Assert.AreEqual(resultPriceList.Brl, result.Brl);
            Assert.AreEqual(resultPriceList.Gbp, result.Gbp);
            Assert.AreEqual(resultPriceList.Aud, result.Aud);
        }
    }
}