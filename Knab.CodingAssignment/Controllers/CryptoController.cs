using System.Threading.Tasks;
using Knab.CodingAssignment.ApplicationServices;
using Knab.CodingAssignment.Framework.Security;
using Microsoft.AspNetCore.Mvc;

namespace Knab.CodingAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoPriceService _cryptoPriceService;

        public CryptoController(ICryptoPriceService cryptoPriceService)
        {
            _cryptoPriceService = cryptoPriceService;
        }

        [HttpGet("Prices/Latest")]
        [BruteForceDenial(1.2, 1800)]
        public async Task<CryptoPriceList> GetCryptoPriceList(string symbol)
        {
            return await _cryptoPriceService.GetPrices(symbol);
        }
    }
}
