using System;

namespace TradeX
{
    class Program
    {
        static void Main(string[] args)
        {
            //Parameters parameters = new Parameters();
            //parameters.ClientID = "U001";
            //parameters.BaseUrl = "https://tradex.saral-info.com:30001";
            //parameters.WebSocket = "wss://tradex.saral-info.com:30001/";
            Client xts = new Client("U001", "https://tradex.saral-info.com:30001",1, "wss://tradex.saral-info.com:30001/", onOrder, onTrade);
            LoginRequest loginReq = new LoginRequest();
            loginReq.app_key = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOiIxNzQyNTY0NzQ0IiwiaXNzIjoiU2FSQUxpTkZvIiwiZXhwIjoiMTc3NDA1MTIwMCIsImF1ZCI6IlUwMDEiLCJqdGkiOiI0NSIsImZsZyI6IjY0In0.U3Ld1pa73eH8HyYuTY4LGc3jJbhkLc23Sba4aR3hTg4";
            loginReq.secret_key = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOiIxNzQyNTY0NzQ0IiwiaXNzIjoiU2FSQUxpTkZvIiwiZXhwIjoiMTc3NDA1MTIwMCIsImF1ZCI6IlUwMDEiLCJqdGkiOiI0NSIsImZsZyI6IjEyOCIsInNjcCI6IjAiLCJzb3VyY2UiOiJTb2xvIiwid2hpdGVsaXN0IjoiIiwidHJkIjoiMjgxNSIsInByZCI6IjE1In0.vQgk1xjxc_QQ4wVCl4esk5qdVmEBIuOtRzS0VPTIXjk";
            loginReq.source = "Test";
            loginReq.user_id = "U001";
            LoginResponseWrapper login = xts.LoginAsync(loginReq).Result;
            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }

        public static void onOrder(OrderState orderBook)
        {
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(orderBook));
        }

        public static void onTrade(Trade tradeBook)
        {
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(tradeBook));
        }
    }
}
