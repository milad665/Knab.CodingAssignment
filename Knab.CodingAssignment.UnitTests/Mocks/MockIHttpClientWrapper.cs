using System.Net;
using System.Net.Http;
using Knab.CodingAssignment.Domain.Exchange;
using Knab.CodingAssignment.Infrastructure;
using Moq;

namespace Knab.CodingAssignment.UnitTests.Mocks
{
    public static class MockIHttpClientWrapper
    {
        public static Mock<IHttpClientWrapper> BadRequestMock()
        {
            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(w => w.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            return mockHttpClientWrapper;
        }

        public static Mock<IHttpClientWrapper> CoinMarketCapOkMock(string symbol, double outputPrice)
        {
            symbol = symbol.ToUpper();
            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    @$"{{""status"":{{""timestamp"":""2021 - 02 - 21T19: 34:53.703Z"",""error_code"":0,""error_message"":null,""elapsed"":33,""credit_count"":1,""notice"":null}},""data"":{{""{symbol}"":{{""id"":1,""name"":""Bitcoin"",""symbol"":""{symbol}"",""slug"":""bitcoin"",""num_market_pairs"":9715,""date_added"":""2013 - 04 - 28T00: 00:00.000Z"",""tags"":[""mineable"",""pow"",""sha - 256"",""store - of - value"",""state - channels"",""coinbase - ventures - portfolio"",""three - arrows - capital - portfolio"",""polychain - capital - portfolio""],""max_supply"":21000000,""circulating_supply"":18634987,""total_supply"":18634987,""is_active"":1,""platform"":null,""cmc_rank"":1,""is_fiat"":0,""last_updated"":""2021 - 02 - 21T19: 33:02.000Z"",""quote"":{{""USD"":{{""price"":{outputPrice},""volume_24h"":57413332130.00193,""percent_change_1h"":0.99715411,""percent_change_24h"":2.06562598,""percent_change_7d"":19.86497157,""percent_change_30d"":78.89905885,""market_cap"":1085721307479.0636,""last_updated"":""2021 - 02 - 21T19: 33:02.000Z""}}}}}}}}}}")
            };
            mockHttpClientWrapper.Setup(w => w.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);

            return mockHttpClientWrapper;
        }

        public static Mock<IHttpClientWrapper> ExchangeRatesApiOkMock(ExchangeRates outputRates)
        {
            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    @$"{{""rates"":{{""CAD"":1.2609770162,""HKD"":7.753604086,""ISK"":128.1818930719,""PHP"":48.4768102809,""DKK"":6.1263695527,""HUF"":295.3291045391,""CZK"":21.294175797,""GBP"":{outputRates.Gbp},""RON"":4.0166405799,""SEK"":8.2650959717,""IDR"":14082.5521047862,""INR"":72.5055605898,""BRL"":{outputRates.Brl},""RUB"":73.8190130983,""HRK"":6.2426888541,""JPY"":105.2887387758,""THB"":29.9744624763,""CHF"":0.893895708,""EUR"":{outputRates.Eur},""MYR"":4.0405305215,""BGN"":1.6111706071,""TRY"":6.9585633083,""CNY"":6.4517670319,""NOK"":8.4104950984,""NZD"":1.3709531263,""ZAR"":14.613312464,""USD"":1.0,""MXN"":20.3553834748,""SGD"":1.3229261059,""AUD"":{outputRates.Aud},""ILS"":3.2704506137,""KRW"":1103.5423016723,""PLN"":3.6930554411}},""base"":""USD"",""date"":""2021-02-19""}}")
            };
            mockHttpClientWrapper.Setup(w => w.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);

            return mockHttpClientWrapper;
        }
    }
}