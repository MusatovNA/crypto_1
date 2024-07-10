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
using static crypto_1.SolWallet;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Net;

namespace crypto_1
{
    internal class BitWallet
    {
        public class BitcoinSignaturesFetcher
        {

                private readonly HttpClient _httpClient = new HttpClient();
                private readonly string _apiKeyId = "c58dbe7eb48e4f2cb2ada4926fbd609a";

                internal ErrorsClass BitError = new ErrorsClass();


            public BitcoinSignaturesFetcher() 
            {
                     _httpClient.DefaultRequestHeaders.Accept.Clear();
                     _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                     _httpClient.DefaultRequestHeaders.Add("APIKeyID", _apiKeyId);
            }


            public async Task<BitTransHistory> GetTransactions(string publicKey)
            {
                string requestUri = $" https://api.blockcypher.com/v1/btc/main/addrs/{publicKey}/full?limit=50";
                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var transactionHistory = JsonConvert.DeserializeObject<BitTransHistory>(responseBody);
                    return transactionHistory;
                }
                else
                {
                    var transactionHistory = new BitTransHistory();
                    return transactionHistory;  
                }

            }

            public async Task<decimal> GetBalance(string publicKey)
            {

                string requestUri = $"https://api.blockcypher.com/v1/btc/main/addrs/{publicKey}/balance";

                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
               try
                { 
                    string responseContent = await response.Content.ReadAsStringAsync();
                    File.WriteAllText("D://BitBalance.json", responseContent);
                    string balanceInfJson = File.ReadAllText("D://BitBalance.json");
                    BitBalanceDataModel.BitBalanceInf balanceinf = new BitBalanceDataModel.BitBalanceInf();
                    balanceinf = JsonConvert.DeserializeObject<BitBalanceDataModel.BitBalanceInf>(balanceInfJson)!;

                    return balanceinf.balance / 100000000;
                }
                catch
                {
                   return 404;
                 
                }
            }

        }


            
        
    }
}
