using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class ExposuresResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<object> data { get; set; }
    }
}
