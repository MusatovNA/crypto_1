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
using System.IO;
using System.Text.Json;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace crypto_1.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page
    {
        public LoadingPage()
        {
            InitializeComponent();
            ExchangeRate rate = new ExchangeRate();

            /*if (rate.GetExchRate(1, "USD") == 404)
            {
                Loading_Block.FontSize = 20;
                Loading_Block.Text = "there is no connection to Coinmarketcap.com, try again";

            }*/



        }
    }
}
