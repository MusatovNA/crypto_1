using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp5;

namespace crypto_1.Pages
{
    /// <summary>
    /// Логика взаимодействия для InfoPage.xaml
    /// </summary>
    public partial class InfoPage : Page
    {
        public InfoPage()
        {
            InitializeComponent();
            Select_Crypt.Items.Add("Bitcoin");
            Select_Crypt.Items.Add("Etherium");
            Select_Crypt.Items.Add("Solana");

            Select_Crypt.SelectedIndex = 0;

            GridView gridView = new GridView();
            tx_List.View = gridView;

            gridView.Columns.Add(new GridViewColumn { Header = "hash", DisplayMemberBinding = new Binding("Hash") });
            gridView.Columns.Add(new GridViewColumn { Header = "+/- Amount", DisplayMemberBinding = new Binding("amount") });
            gridView.Columns.Add(new GridViewColumn { Header = "date", DisplayMemberBinding = new Binding("Date") });
            gridView.Columns.Add(new GridViewColumn { Header = "fee", DisplayMemberBinding = new Binding("Commission") });
            gridView.Columns.Add(new GridViewColumn { Header = "From", DisplayMemberBinding = new Binding("From") });
            gridView.Columns.Add(new GridViewColumn { Header = "To", DisplayMemberBinding = new Binding("To") });




        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            tx_List.Items.Clear();
            Status_Label.Content = "";
            Task task = tryGetResponce(Enter_Wallet.Text, Select_Crypt.SelectedItem.ToString());

        }
        public async Task tryGetResponce(string publicKey, string typeCrypt)
        {



            switch (typeCrypt)
            {
                case "Bitcoin":

                    BitWallet.BitcoinSignaturesFetcher Bitcheck = new BitWallet.BitcoinSignaturesFetcher();
                    var transactions = new BitTransHistory();
                    transactions = await Bitcheck.GetTransactions(publicKey);

                    if (transactions.txs == null)
                    {
                        Status_Label.Content = "Error";
                    }
                    else
                    {
                        for (int a = 0; a <= transactions.txs.Length; a++)
                        {

                            string senderAddress = transactions.txs[a].inputs.FirstOrDefault()?.addresses.FirstOrDefault();
                            string receiverAddress = transactions.txs[a].outputs.FirstOrDefault()?.addresses.FirstOrDefault();
                            int b;
                            if (senderAddress == publicKey)
                                b = -1;
                            else
                                b = 1;
                            ListViewItem listViewItem = new ListViewItem();
                            listViewItem.Content = new Operations
                            {
                                Hash = transactions.txs[a].hash,
                                amount = transactions.txs[a].total / 100000000 * b,
                                Commission = transactions.txs[a].fees / 100000000,
                                Date = transactions.txs[a].confirmed,
                                From = senderAddress,
                                To = receiverAddress
                            };

                            tx_List.Items.Add(listViewItem);
                        }
                    }

                    break;

                case "Solana":

                    break;

                case "Etherium":
                    EthWallet.EthSignaturesFetcher Ethchek = new EthWallet.EthSignaturesFetcher();
                    var transactionsThree = new EthTransHistory();
                    transactionsThree = await Ethchek.GetEthTransactions(publicKey);

                    if (transactionsThree.status == null)
                    {
                        Status_Label.Content = "Error";
                    }
                    else
                    {
                        for (int a = 0; a < transactionsThree.result.Length; a++)
                        {
                            int b;
                            if (transactionsThree.result[a].from == publicKey)
                                b = -1;
                            else
                                b = 1;
                            string ToAddress;
                            if (transactionsThree.result[a].to == "")
                                ToAddress = transactionsThree.result[a].contractAddress;
                            else
                                ToAddress = transactionsThree.result[a].to;
                            ListViewItem listViewItem = new ListViewItem();
                            listViewItem.Content = new Operations
                            {
                                Hash = transactionsThree.result[a].hash,
                                amount = transactionsThree.result[a].value / 1000000000000000000 * b,
                                Commission = (transactionsThree.result[a].gasPrice / 1000000000) * transactionsThree.result[a].gasUsed / 1000000000,
                                Date = DateTimeOffset.FromUnixTimeSeconds(long.Parse(transactionsThree.result[a].timeStamp)).UtcDateTime,
                                From = transactionsThree.result[a].from,
                                To = ToAddress
                            };



                            tx_List.Items.Add(listViewItem);
                        }
                    }


                    break;
            }

        }
    }
}
