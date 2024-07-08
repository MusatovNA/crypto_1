using System;
using System.Collections.Generic;
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
            Select_Crypt.Items.Add("Solana");
            Select_Crypt.Items.Add("Etherium");
            Select_Crypt.SelectedIndex = 0;

            GridView gridView = new GridView();
            tx_List.View = gridView;

            gridView.Columns.Add(new GridViewColumn { Header = "hash", DisplayMemberBinding = new Binding("Hash") });
            gridView.Columns.Add(new GridViewColumn { Header = "+/- Amount", DisplayMemberBinding = new Binding("amount") });
            gridView.Columns.Add(new GridViewColumn { Header = "date", DisplayMemberBinding = new Binding("Date") });
            gridView.Columns.Add(new GridViewColumn { Header = "fee", DisplayMemberBinding = new Binding("Commission") });



        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            Status_Label.Content = "";
            Task task =  tryGetResponce(Enter_Wallet.Text, Select_Crypt.SelectedItem.ToString());

        }
        public async Task tryGetResponce(string publicKey, string typeCrypt)
        {

            
            SolWallet.SolanaSignaturesFetcher Solcheck = new SolWallet.SolanaSignaturesFetcher();
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
                      for(int a=0; a<=transactions.txs.Length; a++)
                      {
                            ListViewItem listViewItem = new ListViewItem();
                            listViewItem.Content = new Operations { Hash = transactions.txs[a].hash, 
                                                                    amount = transactions.txs[a].total/100000000,
                                                                    Commission = transactions.txs[a].fees/100000000, 
                                                                    Date = transactions.txs[a].confirmed };

                            tx_List.Items.Add(listViewItem);
                        }
                    }

                    break;

                case "Solana":

                break;

                case "Etherium":

                break;
            }

        }
    }
}
