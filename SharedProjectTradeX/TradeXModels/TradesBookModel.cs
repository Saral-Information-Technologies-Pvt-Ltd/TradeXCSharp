using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
   public class TradesBookResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public List<Trade> data { get; set; }
    }
    public class Trade
    {
        public string exchange { get; set; }
        public string code { get; set; }
        public string symbol { get; set; }
        public string series { get; set; }
        public double strike_price { get; set; }
        public string option_type { get; set; }
        public string instrument { get; set; }
        public string client { get; set; }
        public bool is_pro { get; set; }
        public string user { get; set; }
        public string generated_by { get; set; }
        public string api_source { get; set; }
        public string side { get; set; }
        public int traded_qty { get; set; }
        public double traded_price { get; set; }
        public double traded_value { get; set; }
        public int qty_remaining { get; set; }
        public int qty_cumulative { get; set; }
        public DateTime trade_time { get; set; }
        public string product { get; set; }
        public string order_category { get; set; }
        public string order_book { get; set; }
        public string order_validity { get; set; }
        public double order_price { get; set; }
        public int order_qty { get; set; }
        public double order_trigger { get; set; }
        public double average_fill_price { get; set; }
        public string order_status { get; set; }
        public int order_disc_qty { get; set; }
        public DateTime order_entry_at { get; set; }
        public DateTime order_last_modified { get; set; }
        public string trade_no { get; set; }
        public string exchange_order_no { get; set; }
        public int sender_order_no { get; set; }
        public int user_order_no { get; set; }
        public int algol_id { get; set; }
    }
}
