using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_1
{

    class Operations
    {
        public string Hash { get; set; }
        public decimal amount { get; set; }
        public DateTime Date { get; set; }
        public decimal Commission { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class EthTransHistoryDetail
    {
        public string hash { get; set; }
        public string timeStamp { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public decimal value { get; set; }
        public decimal gasPrice { get; set; }
        public decimal gasUsed { get; set; }
        public string contractAddress { get; set; }
    }
    public class EthTransHistory
    {
        public string status { get; set; }
        public string message { get; set; }
        public EthTransHistoryDetail[] result { get; set; }
    }
}
