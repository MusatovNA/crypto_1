using System.Text;
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
using static WpfApp5.EthWallet;
using crypto_1.Pages;
using System.ComponentModel;

public class RestClient;

namespace crypto_1
{
    




    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            WorkSpace.Content = new HomePage();
        }
       

        
        

        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            WorkSpace.Content = new HomePage();
        }
        



        private void Crypto_Portf_Button_Click(object sender, RoutedEventArgs e)
        {
            WorkSpace.Content = new PortfolioPage();
        }




        private void Crypto_Inf_Button_Click(object sender, RoutedEventArgs e)
        {
            WorkSpace.Content = new InfoPage();

        }
    }

        
    }

   





