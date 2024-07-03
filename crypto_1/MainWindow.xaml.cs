﻿using System.Text;
using System.Windows;
using System.Net;
using System.Web;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Reflection.PortableExecutable;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.Xml;
using static crypto_1.SolWallet;
using static crypto_1.BitWallet;

public class RestClient;

namespace crypto_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 




    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {

            decimal Balance, exRate, exRateBalance;
            string rateName;
              
            ExchangeRate rate = new ExchangeRate();
            if (Enter_Wallet_TextBox.Text == "")
            {
                MessageBox.Show("адрес кошелька не введен!", "Ошибка!",button:MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                if (Change_Coin.SelectedIndex == 0)
                {
                    if (Change_Rate.SelectedIndex == 0)
                        rateName = "USD";
                    else
                        rateName = "RUB";
                    BitcoinSignaturesFetcher Bit = new BitcoinSignaturesFetcher();
                    Balance = Bit.GetBalance($"{Enter_Wallet_TextBox.Text}") / 100000000;
                    exRate = rate.GetExchRate(1, rateName);
                    Wallet_Name.Content = rateName;
                    exRateBalance = Balance * exRate;
                   
                    current_Balance.Content = Balance;
                    current_Balance_in_currency.Content = exRateBalance;
                }
                else
                {
                    if (Change_Rate.SelectedIndex == 0)
                        rateName = "USD";
                    else
                        rateName = "RUB";

                    SolanaSignaturesFetcher Solana = new SolanaSignaturesFetcher();
                    Balance = Solana.GetBalance($"{Enter_Wallet_TextBox.Text}");
                    Wallet_Name.Content = rateName;
                    exRate =  rate.GetExchRate(5, rateName) ;
                    exRateBalance = Balance * exRate;
                    current_Balance.Content = Balance;
                    current_Balance_in_currency.Content = exRateBalance;
                   
                }
            }

        }

    }

   


}


