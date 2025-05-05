using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiException(HttpStatusCode statusCode, string message, Exception innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            return $"API Exception: {StatusCode} - {Message}";
        }
    }
}
