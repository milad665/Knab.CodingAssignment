﻿using System.Threading.Tasks;
using Knab.CodingAssignment.Domain.Crypto;
using Knab.CodingAssignment.Domain.Exchange;

namespace Knab.CodingAssignment.ApplicationServices
{
    public class CryptoPriceService : ICryptoPriceService
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly ICryptoPriceRepository _cryptoPriceRepository;

        public CryptoPriceService(IExchangeRateRepository exchangeRateRepository, ICryptoPriceRepository cryptoPriceRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
            _cryptoPriceRepository = cryptoPriceRepository;
        }

        public async Task<CryptoPriceList> GetPrices(string symbol)
        {
            var exchangeRates = await _exchangeRateRepository.GetExchangeRates();
            var cryptoRateInUsd = await _cryptoPriceRepository.GetCryptoPriceInUsd(symbol);
            return CryptoPriceList.FromExchangeRates(symbol, cryptoRateInUsd, exchangeRates);
        }
    }
}