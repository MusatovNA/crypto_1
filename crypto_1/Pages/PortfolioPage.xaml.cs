using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
using static crypto_1.BitWallet;
using static crypto_1.SolWallet;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static WpfApp5.EthWallet;

namespace crypto_1.Pages
{
    /// <summary>
    /// Логика взаимодействия для PortfolioPage.xaml
    /// </summary>
    public partial class PortfolioPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<WalletEntry> Transactions { get; set; }
        public ObservableCollection<CurrencySummary> CurrencySummaries { get; set; }

        public class WalletEntry
        {
            public string Address { get; set; }
            public decimal Balance { get; set; }
            public string CurrencyType { get; set; }
        }

      

        public PortfolioPage()
        {
            InitializeComponent();

            Transactions = new ObservableCollection<WalletEntry>();
            CurrencySummaries = new ObservableCollection<CurrencySummary>();
            TransactionsListView.ItemsSource = Transactions;
            SummaryListView.ItemsSource = CurrencySummaries;

            Crypt_ComboBox.Items.Add("Bitcoin");
            Crypt_ComboBox.Items.Add("Etherium");
            Crypt_ComboBox.Items.Add("Solana");
            Crypt_ComboBox.SelectedIndex = 0;
            Currency_ComboBox.Items.Add("RUB");
            Currency_ComboBox.SelectedIndex = 0;

          

            TransactionsListView.ItemsSource = Transactions;



        }
        public string cryptType, currency;

        public void AddTransaction(string address, decimal balance, string currencyType, decimal exchangeRate)
        {
            var existingTransaction = Transactions.FirstOrDefault(t => t.Address == address);
            if (existingTransaction == null)
            {
                var transaction = new WalletEntry { Address = address, Balance = balance, CurrencyType = currencyType };
                Dispatcher.Invoke(() =>
                {
                    Transactions.Add(transaction);
                    UpdateCurrencySummary(currencyType, balance, exchangeRate);
                });
            }
            else
            {
                MessageBox.Show("such a wallet already exists, the data has been updated", "info", MessageBoxButton.OK, MessageBoxImage.Information);
                Dispatcher.Invoke(() =>
                {
                    existingTransaction.Balance = balance;
                    existingTransaction.CurrencyType = currencyType;
                    //UpdateCurrencySummary(currencyType, balance, exchangeRate);
                });
            }
        }
        public decimal exchangeRate;
        public decimal InCur = 0;
        public decimal totalBalance = 0;
        private void UpdateCurrencySummary(string currencyType, decimal balance, decimal exchangeRate)
        {
             
        var summary = CurrencySummaries.FirstOrDefault(cs => cs.CurrencyType == currencyType);
            if (summary == null)
            {
                summary = new CurrencySummary
                {
                    CurrencyType = currencyType,
                    TotalBalance = balance,
                    ExchangeRateValue = exchangeRate
                };
                InCur += balance * exchangeRate;
                totalBalance += InCur;
                switch (currencyType)
                {
                    case "Bitcoin":
                        BtcInCur_Label.Content = InCur;
                        break;

                    case "Etherium":
                        EthInCur_Label.Content = InCur;
                        break;

                    case "Solana":
                        SolInCur_Label.Content = InCur;
                        break;
                }
                TotalInCur_Label.Content = totalBalance;
                CurrencySummaries.Add(summary);
            }
            else
            {
                summary.TotalBalance += balance;
                InCur += balance * exchangeRate;
                totalBalance += InCur;
                switch (currencyType)
                {
                    case "Bitcoin":
                        BtcInCur_Label.Content = InCur;
                        break;

                    case "Etherium":
                        EthInCur_Label.Content = InCur;
                        break;

                    case "Solana":
                        SolInCur_Label.Content = InCur;
                        break;
                }
                TotalInCur_Label.Content = totalBalance;

            }
            if (summary.ExchangeRateValue != exchangeRate)
            {
                summary.ExchangeRateValue = exchangeRate;
            }
             

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class CurrencySummary : INotifyPropertyChanged
        {
            private decimal _totalBalance;
            private decimal _totalBalanceInSelectedCurrency;
            public string CurrencyType { get; set; } 

            public decimal TotalBalance
            {
                get { return _totalBalance; }
                set
                {
                    if (_totalBalance != value)
                    {
                        _totalBalance = value;
                        OnPropertyChanged(nameof(TotalBalance));
                        UpdateTotalBalanceInSelectedCurrency();
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;


            ExchangeRate exchangeRate = new ExchangeRate();

            public decimal ExchangeRateValue { get; set; }

            public decimal TotalBalanceInSelectedCurrency
            {
                get { return _totalBalanceInSelectedCurrency; }
                set
                {
                    if (_totalBalanceInSelectedCurrency != value)
                    {
                        _totalBalanceInSelectedCurrency = value;
                        OnPropertyChanged(nameof(TotalBalanceInSelectedCurrency));
                    }
                }
            }


            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private void UpdateTotalBalanceInSelectedCurrency()
            {

                ExchangeRateValue *= _totalBalance;
            }

           

        }


            private async Task<decimal> tryGetResponce(string cryptType, string currency)
            {
                   

                decimal Rate;
                ExchangeRate exchangeRate = new ExchangeRate();
                int sryptTypeResp = 0;

                
                switch (cryptType)
                {
                    case "Bitcoin":
                        sryptTypeResp = 1;
                        break;

                    case "Etherium":
                        sryptTypeResp = 2;
                        break;

                    case "Solana":
                        sryptTypeResp = 5;
                        break;

                    default:
                        break;
                }

                Rate = await exchangeRate.GetExchRate(sryptTypeResp, currency);
                if (Rate == 404) { return 404; }
                else
                {
                    return Rate;
                }

            }

       



            private async void Add_Button_Click(object sender, RoutedEventArgs e)
            {
                 cryptType = Crypt_ComboBox.SelectedItem.ToString();
                 currency = Currency_ComboBox.SelectedItem.ToString();
                exchangeRate = await tryGetResponce(Crypt_ComboBox.SelectedItem.ToString(), Currency_ComboBox.SelectedItem.ToString());
                 

                 switch (Crypt_ComboBox.SelectedItem.ToString())
                 {
                     case "Bitcoin":
                         var bitBalance = new BitcoinSignaturesFetcher();
                         if (await bitBalance.GetBalance(Wallet_TextBox.Text) == 404) 
                            MessageBox.Show("Can't find this wallet", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                         else 
                         {
                             AddTransaction(Wallet_TextBox.Text,
                                     await bitBalance.GetBalance(Wallet_TextBox.Text),
                                     Crypt_ComboBox.SelectedItem.ToString(),
                                     exchangeRate);
                         }
                     break;

                     case "Etherium":
                        var ethBalance = new EthSignaturesFetcher();
                        if (await ethBalance.GetBalance(Wallet_TextBox.Text) == 404)
                            MessageBox.Show("Can't find this wallet", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                        {
                            AddTransaction(Wallet_TextBox.Text,
                                    await ethBalance.GetBalance(Wallet_TextBox.Text),
                                    Crypt_ComboBox.SelectedItem.ToString(),
                                    exchangeRate);
                        }
                    break;

                     case "Solana":
                    var solBalance = new SolanaSignaturesFetcher();
                         if (await solBalance.GetBalance(Wallet_TextBox.Text) == 404)
                             MessageBox.Show("Can't find this wallet", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                         else
                         {
                             AddTransaction(Wallet_TextBox.Text,
                                     await solBalance.GetBalance(Wallet_TextBox.Text),
                                     Crypt_ComboBox.SelectedItem.ToString(),
                                     exchangeRate);
                         }
                    break;

                     default:
                         break;
                 }

        }

            
    }
}
