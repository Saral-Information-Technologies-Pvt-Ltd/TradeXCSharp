using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{

    //nothing in request only { }
    public class MarginCalculatorResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
    