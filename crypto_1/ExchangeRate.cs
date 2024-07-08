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
        private static string API_KEY = "b79cab96-015f-4f21-bfba-6dcf5edb796c";

        //"b79cab96-015f-4f21-bfba-6dcf5edb796c";

        public async Task<ExchangeRateClass.ExchangeRateInf> GetExchRate(int cryptType, string currency)
        {

            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = $"{cryptType}";
            queryString["limit"] = "1";
            queryString["convert"] = currency;
            URL.Query = queryString.ToString();
            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            try
            {
                var json = client.DownloadString(URL.ToString());
                File.WriteAllText("D://exchangeRateInfo.json", json);
                string RateInfjson = File.ReadAllText("D://exchangeRateInfo.json");
                ExchangeRateClass.ExchangeRateInf exchangeRateInfo = new ExchangeRateClass.ExchangeRateInf();
                exchangeRateInfo = JsonConvert.DeserializeObject<ExchangeRateClass.ExchangeRateInf>(RateInfjson)!;      
                return exchangeRateInfo;
               
            }
            catch
            {
                ExchangeRateClass.ExchangeRateInf exchangeRateInfo = new ExchangeRateClass.ExchangeRateInf();
                return exchangeRateInfo;

            }
        }
     

    }
}
