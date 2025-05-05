using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class UserProfileResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public UserProfile data { get; set; }
    }

    public class UserProfile
    {
        public string client_id { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string trading_allowed { get; set; }
        public string products_allowed { get; set; }
        public string pan { get; set; }
        public string dp_id { get; set; }
        public string beneficiary_id { get; set; }
        public bool has_poa { get; set; }
    }
}
