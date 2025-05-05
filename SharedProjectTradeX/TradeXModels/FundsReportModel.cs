using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class FundsReportResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public List<FundsReport> data { get; set; }
    }

    public class FundsReport
    {
        public string client_id { get; set; }
        public string limit_id { get; set; }
        public string cash { get; set; }
        public string adhoc { get; set; }
        public string payin { get; set; }
        public string collateral { get; set; }
        public string cnc_sell_benefit { get; set; }
        public string payout { get; set; }
        public string costs { get; set; }
        public string margin_used { get; set; }
        public string margin_available { get; set; }
        public string cash_available { get; set; }
    }
}

