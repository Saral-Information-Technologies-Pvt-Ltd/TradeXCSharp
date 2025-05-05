using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class NetPositionsResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public List<Net> data { get; set; }
    }

    public class Net
    {
        public string client { get; set; }
        public string exchange { get; set; }
        public string code { get; set; }
        public string instrument { get; set; }
        public string symbol { get; set; }
        public string series { get; set; }
        public double strike_price { get; set; }
        public string option_type { get; set; }
        public string product { get; set; }
        public int lot_size { get; set; }
        public double multiplier { get; set; }
        public double buy_avg { get; set; }
        public int buy_qty { get; set; }
        public double buy_value { get; set; }
        public double sell_avg { get; set; }
        public int sell_qty { get; set; }
        public double sell_value { get; set; }
        public double net_price { get; set; }
        public int net_qty { get; set; }
        public double net_value { get; set; }
        public double mtm { get; set; }
        public double unrealized_mtm { get; set; }
        public double realized_mtm { get; set; }
        public double market_price { get; set; }
        public double close_price { get; set; }
        public double breakeven_point { get; set; }
        public double intrinsic_value { get; set; }
        public double extrinsic_value { get; set; }
    }
}
