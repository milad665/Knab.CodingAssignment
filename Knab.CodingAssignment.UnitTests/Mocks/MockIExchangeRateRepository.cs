using Knab.CodingAssignment.Domain.Exchange;
using Moq;

namespace Knab.CodingAssignment.UnitTests.Mocks
{
    public static class MockIExchangeRateRepository
    {
        public static Mock<IExchangeRateRepository> ValidExchangeRatesRepository(ExchangeRates outputRates)
        {
            var mock = new Mock<IExchangeRateRepository>();
            mock.Setup(w => w.GetExchangeRates())
                .ReturnsAsync(outputRates);

            return mock;
        }
    }
}