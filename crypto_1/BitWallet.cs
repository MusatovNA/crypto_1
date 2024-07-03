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

namespace crypto_1
{
    internal class BitWallet
    {
        public class BitcoinSignaturesFetcher
        {

                private readonly HttpClient _httpClient = new HttpClient();
                private readonly string _apiKeyId = "987172289ed14324945534c5ad537071";

               

                public BitcoinSignaturesFetcher() 
                {
                     _httpClient.DefaultRequestHeaders.Accept.Clear();
                     _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                     _httpClient.DefaultRequestHeaders.Add("APIKeyID", _apiKeyId);
                }
               

                
                private async Task GetBitGetWalletBalanceAsync(string publicKey)
                {
                    string requestUri = $"https://api.blockcypher.com/v1/btc/main/addrs/{publicKey}/balance";
       
                    HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        File.WriteAllText("D://BitBalance.json", responseContent);
                    }
                    else
                    {
                        //тут будет отчет об ишбке
                    }
                }
                

                
                public decimal GetBalance(string publicKey)
                {
                    var fetcher = new BitcoinSignaturesFetcher();
                    fetcher.GetBitGetWalletBalanceAsync(publicKey);
                    
                    string balanceInfJson = File.ReadAllText("D://BitBalance.json");
                    BitBalanceDataModel.BitBalanceInf balanceinf = new BitBalanceDataModel.BitBalanceInf();
                    balanceinf = JsonConvert.DeserializeObject<BitBalanceDataModel.BitBalanceInf>(balanceInfJson)!;
                    
                    return balanceinf.balance;


                }

        }


            
        
    }
}
