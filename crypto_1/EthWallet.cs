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
using System.Net;
using AtomicCore.BlockChain.ExplorerAPI;
using crypto_1;
namespace WpfApp5
{
    internal class EthWallet
    {

        public class EthSignaturesFetcher
        {
            private readonly HttpClient _httpClient = new HttpClient();
            private readonly string _apiKeyId = "MXDERSH8RUN7AD9X15KDM1FW5MF4TBZBFT";

            public EthSignaturesFetcher()
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("APIKeyID", _apiKeyId);

            }
            public async Task<decimal> GetBalance(string publicKey)
            {
                
                var requestUri = $"https://api.etherscan.io/api?module=account&action=balance&address={publicKey}&tag=latest&apikey={_apiKeyId}";

                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        EthBalanceDataModel balanceinf = new EthBalanceDataModel();
                        balanceinf = JsonConvert.DeserializeObject<EthBalanceDataModel>(responseContent)!;
                        return balanceinf.result / 1000000000000000000;
                    }
                    catch
                    {
                        return 404;
                    }
                }
                else
                {
                    return 404;

                }
            }
            public async Task<EthTransHistory> GetEthTransactions(string publicKey)
            {
                string requestUri = $"https://api.etherscan.io/api?module=account&action=txlist&address={publicKey}&startblock=0&endblock=99999999&page=1&offset=1000&sort=desc&apikey={_apiKeyId}";

                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<EthTransHistory>(responseContent);

                    if (apiResponse.status == "1") 
                    {
                        var transactionHistory = apiResponse.result;
                        return apiResponse;
                        
                    }
                    else 
                    {
                        var transactionHistore = new EthTransHistory();
                        return transactionHistore;
                    }


                }
                else 
                {
                    var transactionHistory = new EthTransHistory();
                    return transactionHistory;
                }
                
            }
        }
    }
}
