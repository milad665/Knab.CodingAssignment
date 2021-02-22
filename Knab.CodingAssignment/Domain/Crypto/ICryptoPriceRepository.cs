using System.Threading.Tasks;

namespace Knab.CodingAssignment.Domain.Crypto
{
    public interface ICryptoPriceRepository
    {
        Task<double> GetCryptoPriceInUsd(string symbol);
    }
}