using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class MarginsRequest
    {
        public string client { get; set; }
    }

    public class MarginsResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
