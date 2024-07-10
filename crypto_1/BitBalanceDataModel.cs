using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_1
{
    internal class BitBalanceDataModel
    {

        public class BitBalanceInf
        {
            public string address { get; set; }
            public long total_received { get; set; }
            public long total_sent { get; set; }
            public decimal balance { get; set; }
            public int unconfirmed_balance { get; set; }
            public long final_balance { get; set; }
            public int n_tx { get; set; }
            public int unconfirmed_n_tx { get; set; }
            public int final_n_tx { get; set; }
        }

    }
}
