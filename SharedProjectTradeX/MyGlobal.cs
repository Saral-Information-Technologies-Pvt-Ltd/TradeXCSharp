using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public static class MyGlobal
    {


        public static void IsClientIDValid(string ClientID)
        {
            // Validate ClientID
            if (string.IsNullOrWhiteSpace(ClientID))
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Client ID is required.");
            }

            if (ClientID.Length < 4 || ClientID.Length > 10)
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Client ID must be between 4 and 10 characters.");
            }
        }

        public static void IsOrderFilterValid(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Filter is required.");
            }

            if (!Enum.TryParse<OrderFilterType>(filter, true, out _))
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Invalid filter value. Allowed values: All, Pending, Unconfirmed, Cancelled, Rejected, Failed, Executed.");
            }
        }

        public static void IsNetPositionFilterValid(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Filter is required.");
            }

            if (!Enum.TryParse<NetPositionFilterType>(filter, true, out _))
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Invalid filter value. Allowed values: All, Opening, Todays.");
            }
        }

    }


    public enum SideType
    {
        Buy,
        Sell
    }

    public enum ExchangeType 
    {
        NseCm,
        NseFO,
        NseCD,
        NseCO,
        Bse,
        BseFO,
        BseCD,
        BseCO,
        MCX,
        Ncdex
    }

    public enum BookType
    {
        RL,  // Regular Order
        SL,  // Stop Loss Order
        PO,  // Pre Open Order
        CA2  // Custom Order Type
    }

    public enum ProductType
    {
        CNC,
        Intraday,
        Normal,
        MTF
    }

    public enum ValidityType
    {
        Day,
        EOSES,
        EOD,
        IOC
    }

    public enum OrderFilterType
    {
        All,
        Pending,
        Unconfirmed,
        Cancelled,
        Rejected,
        Failed,
        Executed
    }
    
    public enum NetPositionFilterType
    {
        All,
        Opening,
        Todays
    }
}
