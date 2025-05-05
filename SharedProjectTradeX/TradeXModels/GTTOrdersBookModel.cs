using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class GTTOrdersBookResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public List<GttOrderState> data { get; set; }
    }

}
