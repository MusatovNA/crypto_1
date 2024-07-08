using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_1
{
    class BitTransactioinSignature
    {
        public string hash { get; set; }
        public decimal total { get; set; }
        public decimal fees { get; set; }
        public DateTime confirmed { get; set; }
        public Input[] inputs { get; set; }
        public Output[] outputs { get; set; }
    }
    public class Input
    {
        public string[] addresses { get; set; }
    }

    public class Output
    {
        public string[] addresses { get; set; }
    }

}
