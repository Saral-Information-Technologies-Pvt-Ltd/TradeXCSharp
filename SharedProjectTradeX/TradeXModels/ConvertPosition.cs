using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class ConvertPositionRequest
    {
        public string client { get; set; }
        public string code { get; set; }
        public string exchange { get; set; }
        public string new_product { get; set; }
        public string old_product { get; set; }
        public int qty { get; set; }
        public string side { get; set; }
    }

    public class ConvertPositionResponse
    {
        
    public int status { get; set; }
        public string message { get; set; }
        public ConvertPositionData data { get; set; }
    }

    public class ConvertPositionData
    {
        public string status { get; set; }
        public int user_order_no { get; set; }
        public string message { get; set; }
    }
}
