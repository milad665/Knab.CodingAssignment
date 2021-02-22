using System.Threading.Tasks;

namespace Knab.CodingAssignment.ApplicationServices
{
    public interface ICryptoPriceService
    {
        Task<CryptoPriceList> GetPrices(string symbol);
    }
}