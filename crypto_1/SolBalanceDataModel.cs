using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_1
{
    public class SolBalanceDataModel
    {
        public class SolBalanceInf
        {
            public decimal balance { get; set; }
            public object decimals { get; set; }
            public string network { get; set; }
            public string unit { get; set; }
        }

    }
}
