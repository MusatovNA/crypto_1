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
using System.Windows.Shapes;

namespace crypto_1
{
    /// <summary>
    /// Логика взаимодействия для LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {

        public LoadingWindow()
        {
            InitializeComponent();
            Task task = CheckConnectiond();
          
        }

        public bool haveErrors = false;
        public async Task CheckConnectiond()
        {
            
            ExchangeRate rate = new ExchangeRate();
            BitWallet.BitcoinSignaturesFetcher Bitcheck = new BitWallet.BitcoinSignaturesFetcher();
            SolWallet.SolanaSignaturesFetcher Solcheck = new SolWallet.SolanaSignaturesFetcher();

            if (await rate.GetExchRate(1, "USD") == null) { Coinmarket_Status.Content = "fail"; haveErrors = true; }
            else Coinmarket_Status.Content = "succes";
            if (await Bitcheck.GetBalance("34xp4vRoCGJym3xR7yCVPFHoCNxv4Twseo") == 404) { Bitcoin_Status.Content = "fail"; haveErrors = true; }
            else Bitcoin_Status.Content = "succes";

            if ( await Solcheck.GetBalance("8PjJTv657aeN9p5R2WoM6pPSz385chvTTytUWaEjSjkq") == 404) { Solana_Status.Content = "fail"; haveErrors = true; }
            else Solana_Status.Content = "succes";

            if (haveErrors == false)
            {
                MainWindow start = new MainWindow();
                start.Show();
                this.Close();
            }
            else
            {
                Retry_Button.Visibility = Visibility.Visible;
            }

        }

        private void RetryClick(object sender, RoutedEventArgs e)
        {
            Coinmarket_Status.Content = "";
            Bitcoin_Status.Content = "";
            Solana_Status.Content = "";
            Task task = CheckConnectiond();

        }
    }
}
