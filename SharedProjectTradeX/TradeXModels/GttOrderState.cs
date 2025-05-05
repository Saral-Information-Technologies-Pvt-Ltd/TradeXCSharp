using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace TradeX
{
    public class NewGttOrderRequest
    {
        [Required(ErrorMessage = "Client is required.")]
        public string client { get; set; }

        [Required(ErrorMessage = "Exchange is required.")]
        [EnumDataType(typeof(ExchangeType), ErrorMessage = "Invalid Exchange Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExchangeType exchange { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        public string code { get; set; }

        [Required(ErrorMessage = "Side is required.")]
        [EnumDataType(typeof(SideType), ErrorMessage = "Side must be either 'Buy' or 'Sell'.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SideType side { get; set; }

        [Required(ErrorMessage = "Product is required.")]
        [EnumDataType(typeof(ProductType), ErrorMessage = "Invalid Product Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int qty { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Main trigger price must be 0 or greater.")]
        public double main_trigger_price { get; set; }

        [Required(ErrorMessage = "Main order price is required.")]
        public string main_order_price { get; set; }

        [Required(ErrorMessage = "Main state is required.")]
        public string main_state { get; set; }

        [Required(ErrorMessage = "Price condition is required.")]
        public string price_condition { get; set; }

        [Required(ErrorMessage = "Stop state is required.")]
        public string stop_state { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Stop trigger price cannot be negative.")]
        public double stop_trigger_price { get; set; }

        [Required(ErrorMessage = "Stop order price is required.")]
        public string stop_order_price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Trail gap cannot be negative.")]
        public double trail_gap { get; set; }

        [Required(ErrorMessage = "Target state is required.")]
        public string target_state { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Target trigger price cannot be negative.")]
        public double target_trigger_price { get; set; }

        [Required(ErrorMessage = "Target order price is required.")]
        public string target_order_price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Sender Order cannot be negative.")]
        public int sender_order_no { get; set; }

        // Method to validate the model before processing
        public void Validate()
        {
            var validationContext = new ValidationContext(this);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ApiException(HttpStatusCode.BadRequest, $"Validation failed: {errorMessages}");
            }
        }
    }

    public class NewGttOrderResponse
    {

        public Int32 status { get; set; }


        public string message { get; set; }

        public GttOrderState data { get; set; }
    }

    public class GttOrderState
    {

        public string client { get; set; }

        public string modified_by { get; set; }


        public string created_by { get; set; }


        public string exchange { get; set; }


        public string code { get; set; }


        public string symbol { get; set; }


        public string series { get; set; }


        public string strike { get; set; }


        public string option_type { get; set; }


        public string side { get; set; }


        public string product { get; set; }


        public int qty { get; set; }


        public double main_trigger_price { get; set; }


        public string main_order_price { get; set; }


        public string main_state { get; set; }


        public string price_condition { get; set; }


        public string stop_state { get; set; }


        public double stop_trigger_price { get; set; }


        public string stop_order_price { get; set; }


        public double trail_gap { get; set; }

 
        public string target_state { get; set; }


        public double target_trigger_price { get; set; }


        public string target_order_price { get; set; }

        public double trail_distance { get; set; }

        public DateTime created_at { get; set; }


        public DateTime last_modified { get; set; }


        public int gtt_order_no { get; set; }


        public string module { get; set; }


        public int filled_qty { get; set; }


        public double filled_value { get; set; }


        public int exit_qty { get; set; }


        public double exit_value { get; set; }

 
        public string reason { get; set; }

  
        public int flags { get; set; }

        public string api_source { get; set; }

   
        public int sender_order_no { get; set; }
    }
}
