using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{

    public sealed class Client 
    {
#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private readonly HttpService httpService;
#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private readonly Parameters parameters;
#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private readonly WebSocketService webSocketService;


        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="xClientID">Client ID.</param>
        /// <param name="xBaseUrl">Base URL for the API.</param>
        /// <param name="xBaseUrlVersion">API version number.</param>
        /// <param name="xWebSocketUrl">WebSocket URL.</param>
        /// <param name="xOrderBook">Callback for order book updates.</param>
        /// <param name="xTradeBook">Callback for trade book updates.</param>
        public Client(string xClientID, string xBaseUrl,int xBaseUrlVersion, string xWebSocketUrl, Action<OrderState> xOrderBook, Action<Trade> xTradeBook)
        {
            if(string.IsNullOrEmpty(xClientID) || string.IsNullOrEmpty(xBaseUrl) || xBaseUrlVersion <= 0 || string.IsNullOrEmpty(xWebSocketUrl))
            {
                return;
            }
            parameters = new Parameters(xClientID, xBaseUrl, xBaseUrlVersion, xWebSocketUrl);
            httpService = new HttpService(parameters);
            webSocketService = new WebSocketService(parameters, xOrderBook, xTradeBook);
        }

        /// 1
        /// <summary>
        /// Logs in the user and establishes a WebSocket connection.
        /// </summary>
        /// <param name="requestModel">Login request data.</param>
        /// <returns>Login response with token and user info.</returns>
        public async Task<LoginResponseWrapper> LoginAsync(LoginRequest requestModel)
        {
            requestModel.Validate();

            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.Login, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                LoginResponseWrapper model = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponseWrapper>(content);
                httpService.SetToken(model.data.token);
                if (webSocketService != null)
                {
                    webSocketService.ConnectionAsync(model.data.token).Wait();
                }
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode,errorMessage);
            }
        }

        // 2
        /// <summary>
        /// Logs out the user and disconnects the WebSocket.
        /// </summary>
        /// <param name="ClientID">User client ID.</param>
        /// <returns>Logout response object.</returns>
        public async Task<LogoutResponse> LogoutAsync(string ClientID)
        {
            MyGlobal.IsClientIDValid(ClientID);

            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.Logout + "?ClientID=" + ClientID, "");
            if (httpResponse.IsSuccessStatusCode)
            {
                if (webSocketService != null)
                {
                    webSocketService.DisconnectAsync();
                }
                string content = await httpResponse.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<LogoutResponse>(content);
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 3
        /// <summary>
        /// Retrieves the user profile information.
        /// </summary>
        /// <param name="ClientID">User client ID.</param>
        /// <returns>User profile response.</returns>
        public async Task<UserProfileResponse> GetUserProfileAsync(string ClientID)
        {
            MyGlobal.IsClientIDValid(ClientID);

            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.UserProfile + "?ClientID=" + ClientID, "");
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                UserProfileResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfileResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 4
        /// <summary>
        /// Places a new order.
        /// </summary>
        /// <param name="requestModel">Order request model.</param>
        /// <returns>New order response.</returns>
        public async Task<NewOrderResponseWrapper> NewOrderAsync(NewOrderRequest requestModel)
        {
            requestModel.Validate();

            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.NewOrder, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                NewOrderResponseWrapper model = Newtonsoft.Json.JsonConvert.DeserializeObject<NewOrderResponseWrapper>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 5
        /// <summary>
        /// Modifies an existing order.
        /// </summary>
        /// <param name="requestModel">Modify order request data.</param>
        /// <returns>Modified order response.</returns>
        public async Task<ModifyOrderResponseWrapper> ModifyOrderAsync(ModifyOrderRequest requestModel)
        {
            requestModel.Validate();

            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.ModifyOrder, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                ModifyOrderResponseWrapper model = Newtonsoft.Json.JsonConvert.DeserializeObject<ModifyOrderResponseWrapper>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 6
        /// <summary>
        /// Cancels a specific order.
        /// </summary>
        /// <param name="requestModel">Cancel order request model.</param>
        /// <returns>Cancel order response.</returns>
        public async Task<CancelOrderResponseWrapper> CancelOrderAsync(CancelOrderRequest requestModel)
        {
            requestModel.Validate();

            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.CancelOrder, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                CancelOrderResponseWrapper model = Newtonsoft.Json.JsonConvert.DeserializeObject<CancelOrderResponseWrapper>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 7
        /// <summary>
        /// Cancels all orders for the user.
        /// </summary>
        /// <param name="requestModel">Cancel all orders request.</param>
        /// <returns>Cancel all orders response.</returns>
        public async Task<CancelAllOrderResponse> CancelAllOrderAsync(CancelAllOrderRequest requestModel)
        {
            requestModel.Validate();
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.CancelAllOrders, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                CancelAllOrderResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<CancelAllOrderResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 8
        /// <summary>
        /// Places a new GTT (Good Till Triggered) order.
        /// </summary>
        /// <param name="requestModel">New GTT order request.</param>
        /// <returns>New GTT order response.</returns>
        public async Task<NewGttOrderResponse> NewGTTOrderAsync(NewGttOrderRequest requestModel)
        {
            requestModel.Validate();
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.NewGttOrder, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                NewGttOrderResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<NewGttOrderResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 9
        /// <summary>
        /// Modifies a GTT (Good Till Triggered) order.
        /// </summary>
        /// <param name="requestModel">Modify GTT order request.</param>
        /// <returns>Modified GTT order response.</returns>
        public async Task<ModifyGTTOrderResponse> ModifyGTTOrderAsync(ModifyGTTOrderRequest requestModel)
        {
            requestModel.Validate();
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.ModifyGttOrder, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                ModifyGTTOrderResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<ModifyGTTOrderResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 10
        /// <summary>
        /// Cancels a GTT (Good Till Triggered) order.
        /// </summary>
        /// <param name="ClientID">User client ID.</param>
        /// <param name="GttOrderNo">GTT order number to cancel.</param>
        /// <returns>Cancel GTT order response.</returns>
        public async Task<CancelGTTOrderResponse> CancelGTTOrderAsync(string ClientID,Int32 GttOrderNo)
        {
            MyGlobal.IsClientIDValid(ClientID);

            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.CancelGttOrder + "?ClientID=" + ClientID + "&GttOrderNo=" + GttOrderNo, "");
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                CancelGTTOrderResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<CancelGTTOrderResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 11
        /// <summary>
        /// Executes a basket order based on the provided request model.
        /// </summary>
        /// <param name="requestModel">Request model containing basket details.</param>
        /// <returns>Response containing the result of basket execution.</returns>
        public async Task<ExecuteBasketResponse> ExecuteBasketAsync(ExecuteBasketRequest requestModel)
        {
            requestModel.Validate();
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.ExecuteBasket, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                ExecuteBasketResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<ExecuteBasketResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 12
        /// <summary>
        /// Retrieves the order book for a client using a specified filter.
        /// </summary>
        /// <param name="ClientID">The client ID.</param>
        /// <param name="Filter">The filter to apply (e.g., All, Pending).</param>
        /// <returns>Response containing order book details.</returns>
        public async Task<OrderBookResponse> GetOrderBookAsync(string ClientID, string Filter)
        {
            MyGlobal.IsClientIDValid(ClientID);
            MyGlobal.IsOrderFilterValid(Filter);

            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.OrderBook + "?ClientID=" + ClientID + "&Filter=" + Filter, "");
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                OrderBookResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderBookResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 13
        /// <summary>
        /// Checks the status of an order.
        /// </summary>
        /// <param name="requestModel">Request model containing order status details.</param>
        /// <returns>Response with current order status information.</returns>
        public async Task<OrderStatusResponse> OrderStatusAsync(OrderStatusRequest requestModel)
        {
            requestModel.Validate();
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.OrderStatus, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                OrderStatusResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderStatusResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 14
        /// <summary>
        /// Retrieves all GTT (Good Till Triggered) orders for a client.
        /// </summary>
        /// <param name="ClientID">The client ID.</param>
        /// <returns>Response containing GTT orders.</returns>
        public async Task<GTTOrdersBookResponse> GTTOrdersBookAsync(string ClientID)
        {
            MyGlobal.IsClientIDValid(ClientID);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.GTTOrdersBook + "?ClientID="+ClientID, "");
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                GTTOrdersBookResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<GTTOrdersBookResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 15
        /// <summary>
        /// Retrieves the trade book for a client.
        /// </summary>
        /// <param name="ClientID">The client ID.</param>
        /// <returns>Response containing executed trades.</returns>
        public async Task<TradesBookResponse> GetTradesBookAsync(string ClientID)
        {
            MyGlobal.IsClientIDValid(ClientID);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.TradeBook + "?ClientID=" + ClientID, "");
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                TradesBookResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<TradesBookResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 16
        /// <summary>
        /// Retrieves the order history for a client.
        /// </summary>
        /// <param name="requestModel">Request model containing order history filter criteria.</param>
        /// <returns>Response containing historical order data.</returns>
        public async Task<OrderHistoryResponse> GetOrderHistoryAsync(OrderHistoryRequest requestModel)
        {
            requestModel.Validate();
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.OrderHistory, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                OrderHistoryResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderHistoryResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 17
        /// <summary>
        /// Retrieves the holdings for a client.
        /// </summary>
        /// <param name="ClientID">The client ID.</param>
        /// <returns>Response containing client holdings.</returns>
        public async Task<HoldingsResponse> GetHoldingsAsync(string ClientID)
        {
            MyGlobal.IsClientIDValid(ClientID);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.Holdings + "?ClientID=" + ClientID, "");
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                HoldingsResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<HoldingsResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 18
        /// <summary>
        /// Retrieves net positions for a client using the specified filter.
        /// </summary>
        /// <param name="ClientID">The client ID.</param>
        /// <param name="Filter">The filter to apply (e.g., NetWise, All).</param>
        /// <returns>Response containing net position details.</returns>
        public async Task<NetPositionsResponse> GetNetPositionsAsync(string ClientID, string Filter)
        {
            MyGlobal.IsNetPositionFilterValid(Filter);
            MyGlobal.IsClientIDValid(ClientID);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.NetPositions + "?ClientID=" + ClientID + "&Filter=" + Filter, "");
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                NetPositionsResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<NetPositionsResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 19
        /// <summary>
        /// Modifies product attributes for an order.
        /// </summary>
        /// <param name="requestModel">Request model containing product modification details.</param>
        /// <returns>Response containing the result of the modification.</returns>
        public async Task<ProductModificationResponseWrapper> ModifyProductAsync(ModifyProductRequest requestModel)
        {
            requestModel.Validate();
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.ModifyProduct, body);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                ProductModificationResponseWrapper model = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModificationResponseWrapper>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 20
        /// <summary>
        /// Retrieves the funds report for a client.
        /// </summary>
        /// <param name="ClientID">The client ID.</param>
        /// <returns>Response containing funds report.</returns>
        public async Task<FundsReportResponse> GetFundsReportAsync(string ClientID)
        {
            MyGlobal.IsClientIDValid(ClientID);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.FundsReport+"?ClientID="+ ClientID, "");
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                FundsReportResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<FundsReportResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }

        // 21
        /// <summary>
        /// Retrieves exchange status information for a client.
        /// </summary>
        /// <param name="ClientID">The client ID.</param>
        /// <returns>Response containing exchange status.</returns>
        public async Task<ExchangeStatusResponse> GetExchangeStatusAsync(string ClientID)
        {
            MyGlobal.IsClientIDValid(ClientID);
            HttpResponseMessage httpResponse = await httpService.PostAsync(PathBundle.ExchangeStatus + "?ClientID=" + ClientID, "");
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                ExchangeStatusResponse model = Newtonsoft.Json.JsonConvert.DeserializeObject<ExchangeStatusResponse>(content);
                return model;
            }
            else
            {
                string errorMessage = await httpResponse.Content.ReadAsStringAsync();
                throw new ApiException(httpResponse.StatusCode, errorMessage);
            }
        }
    }



    //Interactive
    //class RestApi
    //{


    //    public RestApi(Parameters parameters)
    //    {
    //        //here we shaould get all paraters to create a restful app
    //        //url for rest connection
    //        //callback methods for passing web socket data
    //        //version of the api

    //    }

    //    public void SetToken()
    //    {

    //    }

    //    public LoginResponseModel Login(LoginRequestModel model) {
    //        //create all objects
    //        //like http client
    //        //call authenticate http method

    //        return null;
    //    }
    //    public object VerifyOtp(object credentials)
    //    {            
    //        //we will recieve jwt, which we store
    //        // we are now connected
    //        // should we connect to interactive web socket??
    //        //return this web socket
    //        return null;
    //    }

    //    public object PlaceOrder(object order)
    //    {
    //        return null;
    //    }
    //    public object ModifyOrder(object order)
    //    {
    //        return null;
    //    }
    //    public object CancelOrder(object order)
    //    {
    //        return null;
    //    }
    //    public object GetOrderBook(object order)
    //    {
    //        return null;
    //    }
    //    public object GetTradeBook(object order)
    //    {
    //        return null;
    //    }
    //    public object GetHoldings(object order)
    //    {
    //        return null;
    //    }
    //    public object GetPositions(object order)
    //    {
    //        return null;
    //    }

    //    //-------------------------------------
    //    //web socket methods

    //    public object Subscribe(object order)
    //    {
    //        return null;
    //    }


    //}
}
