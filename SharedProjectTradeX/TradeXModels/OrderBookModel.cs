using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class OrderBookResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public List<OrderState> data { get; set; }
    }

    public class OrderState
    {
        public string exchange { get; set; }
        public string code { get; set; }
        public string symbol { get; set; }
        public string series { get; set; }
        public string instrument { get; set; }
        public double strike_price { get; set; }
        public string option_type { get; set; }
        public string client { get; set; }
        public bool is_pro { get; set; }
        public string user { get; set; }
        public string settlor { get; set; }
        public string api_source { get; set; }
        public string executing_id { get; set; }
        public string generated_by { get; set; }
        public string status { get; set; }
        public string side { get; set; }
        public string book { get; set; }
        public string product { get; set; }
        public string validity { get; set; }
        public double price { get; set; }
        public double trigger { get; set; }
        public double average_fill_price { get; set; }
        public int qty_remaining { get; set; }
        public int qty_traded { get; set; }
        public int disc_qty { get; set; }
        public string flags { get; set; }
        public string reason { get; set; }
        public string gtd { get; set; }
        public DateTime client_entry_time { get; set; }
        public DateTime entry_at { get; set; }
        public DateTime last_modified { get; set; }
        public string exchange_order_no { get; set; }
        public int user_order_no { get; set; }
        public int sender_order_no { get; set; }
        public int auction_number { get; set; }
        public string order_category { get; set; }
        public int algol_id { get; set; }
    }

    public class WS_OrderBook
    {
        public string eventType { get; set; }
        public OrderState data { get; set; }
    }

}

