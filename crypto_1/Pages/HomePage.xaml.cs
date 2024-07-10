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

namespace crypto_1.Pages
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            TryAgain_Button.Visibility = Visibility.Hidden;
            Crypt_ComboBox.Items.Add("Bitcoin");
            Crypt_ComboBox.Items.Add("Etherium");
            Crypt_ComboBox.Items.Add("Solana");
            Crypt_ComboBox.SelectedIndex = 0;
            Сurrency_ComboBox.Items.Add("USD");
            Сurrency_ComboBox.Items.Add("RUB");
            Сurrency_ComboBox.SelectedIndex = 0;
            Task task = tryGetResponce(Crypt_ComboBox.SelectedItem.ToString(), Сurrency_ComboBox.SelectedItem.ToString());
        }


        decimal Rate;

        private async Task<decimal> tryGetResponce(string cryptType, string currency)
        {
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

        private async void Crypt_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Task task = Crypt_ComboBox_SelectionChangedAsync();
        }

       

        private async void Сurrency_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           Task task = Сurrency_ComboBox_SelectionChangedAsync();
        }


        

        private async Task Crypt_ComboBox_SelectionChangedAsync()
        {
            if (Сurrency_ComboBox.SelectedItem != null)
            {
                decimal newRate = await tryGetResponce(Crypt_ComboBox.SelectedItem.ToString(), Сurrency_ComboBox.SelectedItem.ToString());
                tryGetResponce(Crypt_ComboBox.SelectedItem.ToString(), Сurrency_ComboBox.SelectedItem.ToString());
                if (newRate == 404)
                {
                    TryAgain_Button.Visibility = Visibility.Visible;
                    Currency_TextBox.Text = "0";
                    Error_Label.Content = "Error";
                    MessageBox.Show("Check your internet connection or try later", "Error", button: MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else 
                {
                    TryAgain_Button.Visibility = Visibility.Hidden;
                    Currency_TextBox.Text = $"{newRate}";
                }
            }

        }
        private async Task Сurrency_ComboBox_SelectionChangedAsync()
        {
            if (Crypt_ComboBox.SelectedItem != null)
            {
                decimal newRate = await tryGetResponce(Crypt_ComboBox.SelectedItem.ToString(), Сurrency_ComboBox.SelectedItem.ToString());
                if (newRate == 404)
                {
                    TryAgain_Button.Visibility = Visibility.Visible;
                    Currency_TextBox.Text = "0";
                    Error_Label.Content = "Error";
                    MessageBox.Show("Check your internet connection or try later", "Error", button: MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    TryAgain_Button.Visibility = Visibility.Hidden;
                    Currency_TextBox.Text = $"{newRate}";
                }
            }

        }
        private void TryAgain_Button_Click(object sender, RoutedEventArgs e)
        {
            Task task = Crypt_ComboBox_SelectionChangedAsync();
        }

        private void Currency_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void Crypt_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       
    }
}
