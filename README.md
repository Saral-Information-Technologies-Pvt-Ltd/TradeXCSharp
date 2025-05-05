
# TradeX - C# SDK

TradeX is a C# SDK that interacts with the Saral Info Trading API via REST and WebSocket for login, order placement, and real-time trade/order updates.

---

## 📦 Project Structure

```
TradeX/
│
├── SaralInteractiveApi/              # ASP.NET Core solution containing Web API implementation for Saral Info
├── SaralInteractiveApiNetFramework/  # .NET Framework version of the API implementation (for legacy support)
├── SaralInteractiveApiTest/          # Test harness and manual testing project for API methods and responses
├── SharedProjectTradeX/              # Shared project with models, enums, core client logic and WebSocket handlers (Auto synced when either files are edited)
└── README.md
```

---

## 🚀 Quick Start

### 1. Clone the Repo

```bash
git clone https://github.com/Saral-Information-Technologies-Pvt-Ltd/TradeXCSharp.git
cd TradeXCSharp
```

### 2. Initialize Client and Login

```csharp
Using TradeX;

Client client = new Client("Test1", "https://tradex.saral-info.com:30001", 1, "wss://tradex.saral-info.com:30001", null, null);

LoginRequest request = new LoginRequest
{
    app_key = "YOUR_APP_KEY",
    secret_key = "YOUR_SECRET_KEY",
    source = "Test",
    user_id = "Test1"
};

client.LoginAsync(request).Wait();
```

---

## 📤 Placing a New Order

```csharp
NewOrderRequest requestModel = new NewOrderRequest
{
    client = "Test1",
    book = BookType.RL,
    order_flag = 0,
    gtd = "",
    validity = ValidityType.Day,
    product = ProductType.Normal,
    disclosed_qty = 0,
    trigger_price = 0,
    algol_id = 0,
    price = 140,
    quantity = 10,
    side = SideType.Sell,
    code = "3499",
    exchange = ExchangeType.NseCm,
    sender_order_no = 1
};

NewOrderResponseWrapper newOrder = client.NewOrderAsync(requestModel).Result;
```

---

## 🔁 WebSocket Callbacks

Implement order/trade event handlers to receive real-time updates:

```csharp
public static void onOrderBook(OrderState order)
{
    // handle order state change
}

public static void onTradeBook(Trade trade)
{
    // handle trade confirmation
}
```

You can pass these callbacks to the `Client` constructor:

```csharp
Client client = new Client("Test1", baseUrl, version, websocketUrl, onOrderBook, onTradeBook);
```

---

## 🛠 Requirements

- .NET Framework 4.7.2+ or .NET Core 3.1+
- Visual Studio 2019 or later
- Newtonsoft.Json 4.0.30319 (From NuGet Package Manager)
- System Management 9.0.4 (From NuGet Package Manager)

---

## 📃 License

This project is licensed under MIT License. Please contact [Saral Info](https://saral-info.com) for API access.
