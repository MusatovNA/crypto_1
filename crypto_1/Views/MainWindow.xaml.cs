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
    /// 




    public class data_
    {

        public class Rootobject
        {
            public Status status { get; set; }
            public Datum[] data { get; set; }
        }

        public class Status
        {
            public DateTime timestamp { get; set; }
            public int error_code { get; set; }
            public object error_message { get; set; }
            public int elapsed { get; set; }
            public int credit_count { get; set; }
            public object notice { get; set; }
            public int total_count { get; set; }
        }

        public class Datum
        {
            public int id { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
            public string slug { get; set; }
            public int num_market_pairs { get; set; }
            public DateTime date_added { get; set; }
            public string[] tags { get; set; }
            public int max_supply { get; set; }
            public int circulating_supply { get; set; }
            public int total_supply { get; set; }
            public bool infinite_supply { get; set; }
            public object platform { get; set; }
            public int cmc_rank { get; set; }
            public object self_reported_circulating_supply { get; set; }
            public object self_reported_market_cap { get; set; }
            public object tvl_ratio { get; set; }
            public DateTime last_updated { get; set; }
            public Quote quote { get; set; }
        }

        public class Quote
        {
            public RUB RUB { get; set; }
        }

        public class RUB
        {
            public float price { get; set; }
            public float volume_24h { get; set; }
            public float volume_change_24h { get; set; }
            public float percent_change_1h { get; set; }
            public float percent_change_24h { get; set; }
            public float percent_change_7d { get; set; }
            public float percent_change_30d { get; set; }
            public float percent_change_60d { get; set; }
            public float percent_change_90d { get; set; }
            public float market_cap { get; set; }
            public float market_cap_dominance { get; set; }
            public float fully_diluted_market_cap { get; set; }
            public object tvl { get; set; }
            public DateTime last_updated { get; set; }
        }


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
            client.Headers.Add("Accepts", "application/json");

            var json = client.DownloadString(URL.ToString());
            
            resultt.Text += json;

            File.WriteAllText("D://crypto.json", json);
            string obj = File.ReadAllText("D://crypto.json");
            data_.Rootobject newData = new data_.Rootobject();
             newData = JsonConvert.DeserializeObject<data_.Rootobject>(obj)!;
            nameCrypt.Content = newData.data[0].quote.RUB.price;


        }

    }
}