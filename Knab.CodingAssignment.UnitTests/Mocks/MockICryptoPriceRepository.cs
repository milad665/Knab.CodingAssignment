using Knab.CodingAssignment.Domain.Crypto;
using Moq;

namespace Knab.CodingAssignment.UnitTests.Mocks
{
    public static class MockICryptoPriceRepository
    {
        public static Mock<ICryptoPriceRepository> ValidCryptoPriceRepository(double outputPrice)
        {
            var mock = new Mock<ICryptoPriceRepository>();
            mock.Setup(w => w.GetCryptoPriceInUsd(It.IsAny<string>()))
                .ReturnsAsync(outputPrice);

            return mock;
        }
    }
}