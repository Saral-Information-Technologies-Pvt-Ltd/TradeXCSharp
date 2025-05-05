using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using Newtonsoft.Json;
using System.Diagnostics;

namespace TradeX
{
    internal sealed class WebSocketService
    {
#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private Parameters Parameters;

#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private ClientWebSocket Socket;

#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private Action<OrderState> CallBackOrderBook;

#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private Action<Trade> CallBackTradeBook;

#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private CancellationTokenSource _cts;

#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private int _reconnectAttempts = 0;

#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private const int MaxReconnectAttempts = 3;

#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private readonly TimeSpan ReconnectDelay = TimeSpan.FromSeconds(3);

#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private string _token = "";

#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private Type[] models = { typeof(WS_OrderBook), typeof(WS_TradeBook) };

        public WebSocketService(Parameters xParameters, Action<OrderState> xOrderBook, Action<Trade> xTradeBook)
        {
            Socket = new ClientWebSocket();
            Parameters = xParameters;
            CallBackOrderBook = xOrderBook;
            CallBackTradeBook = xTradeBook;
            _cts = new CancellationTokenSource();
        }

        public async Task ConnectionAsync(string token)
        {
            _token = token;
            //while (_reconnectAttempts < MaxReconnectAttempts)
            try
            {
                string webSocketUrl = Parameters.WebSocketUrl + "?token=" + token + "&clientID=" + Parameters.ClientID;
                await Socket.ConnectAsync(new Uri(webSocketUrl), _cts.Token);
                _reconnectAttempts = 0;
                 _ = Task.Run(() => ReceiverAsync());
                    return;
            }catch(Exception ex)
            {
                    //_reconnectAttempts++;

                    //if (_reconnectAttempts >= MaxReconnectAttempts)
                    //{
                        throw new ApiException(System.Net.HttpStatusCode.ServiceUnavailable, "WebSocket Connection Failed: Max reconnect attempts reached. Stopping reconnect attempts.");
                    //}

                    //await Task.Delay(ReconnectDelay); 
            }
        }
        
        public async void DisconnectAsync()
        {
            if (Socket.State != WebSocketState.Open) return;
            _cts.Cancel();
            await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
        }

        public async Task ReceiverAsync()
        {
            try
            {
                if (Socket.State != WebSocketState.Open)
                {
                    //await HandleReconnect();
                    return;
                }

                byte[] buffer = new byte[1024 * 4];
                var result = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string data = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);

                new Thread(() => DeSerializeData(data)).Start();

                await ReceiverAsync();
            }catch(Exception ex)
            {
                //await HandleReconnect();
            }
        }

        private async Task HandleReconnect()
        {
            if (_reconnectAttempts < MaxReconnectAttempts)
            {
                await Task.Delay(ReconnectDelay);
                _reconnectAttempts = 0;
                await ConnectionAsync(_token);
            }
            else
            {
                throw new ApiException(System.Net.HttpStatusCode.ServiceUnavailable, "Reconnect attempts exceeded. WebSocket connection failed permanently. ❌");
            }
        }


        public void DeSerializeData(string data)
        {
            foreach (var modelType in models)
            {
                try
                {
                    object obj = Newtonsoft.Json.JsonConvert.DeserializeObject(data, modelType);
                    if(obj is WS_OrderBook)
                    {
                        WS_OrderBook book = (WS_OrderBook)obj;
                        if (string.Equals(book.eventType, "order"))
                        {
                            CallBackOrderBook(book.data);
                            return;
                        }
                    }
                    if (obj is WS_TradeBook)
                    {
                        WS_TradeBook book = (WS_TradeBook)obj;
                        if (string.Equals(book.eventType, "trade"))
                        {
                            CallBackTradeBook(book.data);
                            return;
                        }
                    }
                }
                catch (JsonException)
                {
                    continue;
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

    }
}
