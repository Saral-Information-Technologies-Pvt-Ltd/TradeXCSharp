using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
   public class ExchangeStatusResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public List<ExchangeStatus> data { get; set; }
    }

    public class ExchangeStatus
    {
        public string exchange { get; set; }
        public bool isConnected { get; set; }
        public string session { get; set; }
    }
}
