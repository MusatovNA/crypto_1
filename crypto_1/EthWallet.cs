using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
namespace WpfApp5
{
    internal class EthWallet
    {

        public class EthSignaturesFetcher
        {
            private readonly HttpClient _httpClient = new HttpClient();
            private readonly string _apiKeyId = "WA2MJSVRZSN7X3JT446DS8D4KFQQ7CM9XF";


            public EthSignaturesFetcher()
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("APIKeyID", _apiKeyId);

            }
            private async Task GetEthGetWalletBalanceAsync(string publicKey)
            {

                var requestUri = $"https://api.etherscan.io/api?module=account&action=balance&address={publicKey}&tag=latest&apikey={_apiKeyId}";

                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    File.WriteAllText("D://EthBalance.json", responseContent);
                }
                else
                {
                    //тут будет отчет об ишбке
                }
            }


            public long GetBalance(string publicKey)
            {
                var fetcher = new EthSignaturesFetcher();
                fetcher.GetEthGetWalletBalanceAsync(publicKey);

                string balanceInfJson = File.ReadAllText("D://EthBalance.json");
                EthBalanceDataModel balanceinf = new EthBalanceDataModel();
                balanceinf = JsonConvert.DeserializeObject<EthBalanceDataModel>(balanceInfJson)!;
             
                return balanceinf.rezult;

            }
        }
    }
}
