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

namespace crypto_1.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class data_
    {

        public string? name { get; set; }
        public string? timestamp { get; set; }
        public string? price { get; set; }

    }
    public class status
    {


        public string? timestamp { get; set; }
      

    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private static string API_KEY = "b79cab96-015f-4f21-bfba-6dcf5edb796c";
        private void connect_Click(object sender, RoutedEventArgs e)
        {

        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["start"] = "1";

            queryString["limit"] = "1";
            queryString["convert"] = "RUB";

            URL.Query = queryString.ToString();

            var client = new WebClient();

            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);

            var json = client.DownloadString(URL.ToString());

            resultt.Text += json;

            StreamWriter file = new StreamWriter("D:\\crypto.json");


            file.Write(client.DownloadString(URL.ToString()));
            file.Close();

           
            var purchaseJson = File.ReadAllText("D:\\crypto.json") ;
            dynamic example = JsonConvert.DeserializeObject(purchaseJson);

            nameCrypt.Content = example;
        }
    }
}