using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    internal sealed class Parameters
    {
        public Parameters()
        {

        }
        public Parameters(string xClientID, string xBaseUrl, int xBaseUrlVersion, string xWebSocketUrl)
        {
            xBaseUrl = xBaseUrl + "/tradexApi/v"+ xBaseUrlVersion + "/";

            this.ClientID = xClientID;
            this.BaseUrl = xBaseUrl;
            this.WebSocketUrl = xWebSocketUrl;
        }
#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif

        public string ClientID { get; set; }
#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif

        public string BaseUrl { get; set; }
#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif

        public string WebSocketUrl { get; set; }
    }
}
