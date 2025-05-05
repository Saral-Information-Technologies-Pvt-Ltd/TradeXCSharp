using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class HoldingsResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public List<Holding> data { get; set; }
    }

    public class Holding
    {
        public string client { get; set; }
        public string isin { get; set; }
        public string nse_name { get; set; }
        public string bse_name { get; set; }
        public string bse_code { get; set; }
        public string nse_code { get; set; }
        public double nse_ltp { get; set; }
        public double bse_ltp { get; set; }
        public double position { get; set; }
        public double free_qty { get; set; }
        public double collateral_qty { get; set; }
        public double pledged_qty { get; set; }
        public double btst_qty { get; set; }
        public double blocked_qty { get; set; }
        public double non_poa_qty { get; set; }
        public double value { get; set; }
        public double collateral_value { get; set; }
        public double buy_price { get; set; }
        public double close_price { get; set; }
        //public int todays_sold_qty { get; set; }
        //public int todays_bought_qty { get; set; }
        //public int pending_sell_qty { get; set; }
    }
}
