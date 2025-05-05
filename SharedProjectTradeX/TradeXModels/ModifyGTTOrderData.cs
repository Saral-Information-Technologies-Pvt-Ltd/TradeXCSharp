
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace TradeX
{
    

    public class ModifyGTTOrderRequest
    {
        [Required(ErrorMessage = "Client is required.")]
        public string client { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "GTT Order Number must be greater than 0.")]
        public int gtt_order_no { get; set; }

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

        [Required(ErrorMessage = "Price condition is required.")]
        public string price_condition { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Main trigger price must be 0 or greater.")]
        public double main_trigger_price { get; set; }

        [Required(ErrorMessage = "Main order price is required.")]
        public string main_order_price { get; set; }

        [Required(ErrorMessage = "Main state is required.")]
        public string main_state { get; set; }

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

    public class ModifyGTTOrderResponse
    {

        public Int32 status { get; set; }


        public string message { get; set; }


        public GttOrderState data { get; set; }
    }
}
