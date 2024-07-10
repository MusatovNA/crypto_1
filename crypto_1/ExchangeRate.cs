using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using static crypto_1.ExchangeRateClass;

namespace crypto_1
{
    internal class ExchangeRate
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private static string API_KEY = "b619b714-e216-4b07-a68d-014b4b8b1099";

        //"b79cab96-015f-4f21-bfba-6dcf5edb796c";

        public async Task<Decimal> GetExchRate(int cryptType, string currency)
        {

            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = $"{cryptType}";
            queryString["limit"] = "1";
            queryString["convert"] = currency;
            URL.Query = queryString.ToString();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", API_KEY);
                client.DefaultRequestHeaders.Add("Accepts", "application/json");

                try
                {
                    var response = await client.GetAsync(URL.ToString());
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    ExchangeRateClass.ExchangeRateInf exchangeRateInfo = JsonConvert.DeserializeObject<ExchangeRateClass.ExchangeRateInf>(json)!;
                    if(currency == "USD")
                         return exchangeRateInfo.data[0].quote.USD.price;
                    else
                        return exchangeRateInfo.data[0].quote.RUB.price;

                }
                catch
                {
                    ExchangeRateClass.ExchangeRateInf exchangeRateInfo = new ExchangeRateClass.ExchangeRateInf();
                    return 404;
                }
            }


        }
    }

}