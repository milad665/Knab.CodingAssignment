using System.Threading.Tasks;

namespace Knab.CodingAssignment.Domain.Exchange
{
    public interface IExchangeRateRepository
    {
        Task<ExchangeRates> GetExchangeRates();
    }
}