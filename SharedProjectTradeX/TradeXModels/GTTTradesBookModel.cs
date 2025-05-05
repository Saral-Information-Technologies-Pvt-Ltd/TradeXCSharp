using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class WS_TradeBook
    {
        public string eventType { get; set; }
        public Trade data { get; set; }
    }
}
