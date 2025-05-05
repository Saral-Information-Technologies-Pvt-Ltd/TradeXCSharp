using System;
using TradeX;

namespace SaralInteractiveApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("K3112", "https://tradex.saral-info.com:30001",1, "wss://tradex.saral-info.com:30001", onOrderBook, onTradeBook);
            LoginRequest request = new LoginRequest();
            request.app_key = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOiIxNzQ0NzIwNDUwIiwiaXNzIjoiU2FyYWwiLCJleHAiOiIxNzc2MjExMjAwIiwiYXVkIjoiSzMxMTIiLCJqdGkiOiI3NSIsImZsZyI6IjY0In0.F6AUk1GT4JFpiA_pp86kFaIrp-tLdJNhNuGspAYUHF0";
            request.secret_key = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOiIxNzQ0NzIwNDUwIiwiaXNzIjoiU2FyYWwiLCJleHAiOiIxNzc2MjExMjAwIiwiYXVkIjoiSzMxMTIiLCJqdGkiOiI3NSIsImZsZyI6IjEyOCIsInNjcCI6IjAiLCJzb3VyY2UiOiJTb2xvIiwid2hpdGVsaXN0IjoiIiwidHJkIjoiMjgxNSIsInByZCI6IjcifQ.YNYk7lfg4oLEX7LmG3BBqMqJpmEOsjvpfGFZeZ4AOq0";
            request.source = "Test";
            request.user_id = "K3112";
            client.LoginAsync(request).Wait();

            NewOrderRequest requestModel = new NewOrderRequest();
            requestModel.client = "K3112";
            requestModel.book = BookType.RL;
            requestModel.order_flag = 0;
            requestModel.gtd = "";
            requestModel.validity = ValidityType.Day;
            requestModel.product = ProductType.Normal;
            requestModel.disclosed_qty = 0;
            requestModel.trigger_price = 0;
            requestModel.algol_id = 0;
            requestModel.price = 140;
            requestModel.quantity = 10;
            requestModel.side = SideType.Sell;
            requestModel.code = "3499";
            requestModel.exchange = ExchangeType.NseCm;
            requestModel.sender_order_no = 1;


            NewOrderResponseWrapper newOrder = client.NewOrderAsync(requestModel).Result;
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }


        //Action<OrderState> xOrderBook, Action<Trade> xTradeBook

        public static void onOrderBook(OrderState order)
        {
            Console.WriteLine(order);
        }

        public static void onTradeBook(Trade trade)
        {
            Console.WriteLine(trade);
        }
        
    }
}
