using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{

    public class StatusResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<StatusResponseData> data { get; set; }

    }
    public class StatusResponseData
    {
        public string exchange { get; set; }
        public bool isConnected { get; set; }
        public string session { get; set; }
    }
}
