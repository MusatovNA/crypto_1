using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Security.Cryptography.Xml;
using System.Net;

namespace crypto_1
{
    internal class SolWallet
    {
        public class SolanaSignaturesFetcher
        {



            private readonly HttpClient _httpClient = new HttpClient();
            private readonly string _apiKeyId = "BqE2LOyxB4vd9JO";
            private readonly string _apiSecretKey = "1D5yp9Xpip0sXuq";
            public ErrorsClass SolError = new ErrorsClass();




            public SolanaSignaturesFetcher()
            {
                SolError.haveErrors = false;
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("APIKeyID", _apiKeyId);
                _httpClient.DefaultRequestHeaders.Add("APISecretKey", _apiSecretKey);
            }

            public async Task<decimal> GetBalance(string publicKey)
            {
                string mintAddress = "";
                var requestContent = new
                {
                    public_key = publicKey,
                    unit = "sol",
                    network = "mainnet-beta",
                    mint_address = string.IsNullOrEmpty(mintAddress) ? null : mintAddress
                };

                string requestUri = "https://api.blockchainapi.com/v1/solana/wallet/balance";

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(requestUri, requestContent);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    File.WriteAllText("D://SolBalance.json", responseContent);
                    string balanceInfJson = File.ReadAllText("D://SolBalance.json");
                    SolBalanceDataModel.SolBalanceInf balanceinf = new SolBalanceDataModel.SolBalanceInf();
                    balanceinf = JsonConvert.DeserializeObject<SolBalanceDataModel.SolBalanceInf>(balanceInfJson)!;
                    return balanceinf.balance;
                }
                else
                {
                    return 404;
                }
            }
            private async Task<string[]> GetTransactionSignatures(string publicKey)
            {
                string requestUri = $"https://api.blockchainapi.com/v1/solana/wallet/mainnet-beta/{publicKey}/transactions";

                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    File.WriteAllText("D://SolTransactions.json", responseContent);
                    string TransactionsInfJson = File.ReadAllText("D://SolTransactions.json");
                    SolTransInfo txSignatures = new SolTransInfo();
                    txSignatures.txSignatures = JsonConvert.DeserializeObject<string[]>(TransactionsInfJson)!;
                    return txSignatures.txSignatures;   
                }
                else
                {
                    SolTransInfo txSignatures = new SolTransInfo();
                    txSignatures.txSignatures[0] = "404";
                    return txSignatures.txSignatures;
                }
            }

            private async Task GetTransactionInfoAsync(string txSignature)
            {
                string requestUri = $"https://api.blockchainapi.com/v1/solana/transaction/mainnet-beta/{txSignature}";

                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    File.WriteAllText("D://SolTransactionsInfo.json", responseContent);
                }
                else
                {
                    //тут будет отчет об ишбке
                }
            }


           

            public async Task GetTransactionsInfo(string txSignature)
            {
                var fetcher = new SolanaSignaturesFetcher();
                await fetcher.GetTransactionInfoAsync(txSignature);

                string balanceInfJson = File.ReadAllText("D://SolTransactionsInfo.json");
                SolTransactionInfoDataModel.SolTransInfo balanceinf = new SolTransactionInfoDataModel.SolTransInfo();
                balanceinf = JsonConvert.DeserializeObject<SolTransactionInfoDataModel.SolTransInfo>(balanceInfJson)!;

               // return balanceinf.balance;

            }

         



            //string txSignature = "3Qt4mvXS8XcNKFDoKb62kVeR4KJPBiEt55gBo7mMpN6bxCiFhZkCdhC7k27tryWWptgdReRLf9SUvYz4DguE3Xsa";





        }
    }
}
