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
            Crypt_ComboBox.Items.Add("Bitcoin");
            Crypt_ComboBox.Items.Add("Etherium");
            Crypt_ComboBox.Items.Add("Solana");
            Crypt_ComboBox.SelectedIndex = 0;
            Сurrency_ComboBox.Items.Add("USD");
            Сurrency_ComboBox.Items.Add("RUB");
            Сurrency_ComboBox.SelectedIndex = 0;
            Task task = tryGetResponce(Crypt_ComboBox.SelectedItem.ToString(), Сurrency_ComboBox.SelectedItem.ToString());
        }


        ExchangeRateClass.ExchangeRateInf Rate = new ExchangeRateClass.ExchangeRateInf();

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
            if (Rate == null) { return 404; }
            else
            {
                if (currency == "USD")
                    return Rate.data[0].quote.USD.price;
                else
                    return Rate.data[0].quote.RUB.price;

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
                Crypt_TextBox.Text = "1";
                Currency_TextBox.Text = $"{newRate}";
            }

        }
        private async Task Сurrency_ComboBox_SelectionChangedAsync()
        {
            if (Crypt_ComboBox.SelectedItem != null)
            {
                decimal newRate = await tryGetResponce(Crypt_ComboBox.SelectedItem.ToString(), Сurrency_ComboBox.SelectedItem.ToString());
                Crypt_TextBox.Text = "1";
                Currency_TextBox.Text = $"{newRate}";
            }

        }
        
        private void Currency_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void Crypt_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       
    }
}
