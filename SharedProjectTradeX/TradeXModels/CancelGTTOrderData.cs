using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    //public class CancelGTTOrderRequest
    //{
    //    [Required(ErrorMessage = "Client is required.")]
    //    public string client { get; set; }

    //    [Required(ErrorMessage = "Modified By is required.")]
    //    public string modified_by { get; set; }

    //    [Required(ErrorMessage = "Created By is required.")]
    //    public string created_by { get; set; }

    //    [Required(ErrorMessage = "Exchange is required.")]
    //    [EnumDataType(typeof(ExchangeType), ErrorMessage = "Invalid Exchange Type.")]
    //    public string exchange { get; set; }

    //    [Required(ErrorMessage = "Code is required.")]
    //    public string code { get; set; }

    //    [Required(ErrorMessage = "Symbol is required.")]
    //    public string symbol { get; set; }

    //    [Required(ErrorMessage = "Series is required.")]
    //    public string series { get; set; }

    //    [Required(ErrorMessage = "Strike is required.")]
    //    public string strike { get; set; }

    //    [Required(ErrorMessage = "Option Type is required.")]
    //    public string option_type { get; set; }

    //    [Required(ErrorMessage = "Side is required.")]
    //    [EnumDataType(typeof(SideType), ErrorMessage = "Side must be either 'Buy' or 'Sell'.")]
    //    public string side { get; set; }

    //    [Required(ErrorMessage = "Product is required.")]
    //    [EnumDataType(typeof(ProductType), ErrorMessage = "Invalid Product Type.")]
    //    public string product { get; set; }

    //    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
    //    public int qty { get; set; }

    //    [Range(0, double.MaxValue, ErrorMessage = "Main trigger price must be 0 or greater.")]
    //    public double main_trigger_price { get; set; }

    //    [Required(ErrorMessage = "Main order price is required.")]
    //    public string main_order_price { get; set; }

    //    [Required(ErrorMessage = "Main state is required.")]
    //    public string main_state { get; set; }

    //    [Required(ErrorMessage = "Price condition is required.")]
    //    public string price_condition { get; set; }

    //    [Required(ErrorMessage = "Stop state is required.")]
    //    public string stop_state { get; set; }

    //    [Range(0, double.MaxValue, ErrorMessage = "Stop trigger price cannot be negative.")]
    //    public double stop_trigger_price { get; set; }

    //    [Required(ErrorMessage = "Stop order price is required.")]
    //    public string stop_order_price { get; set; }

    //    [Range(0, double.MaxValue, ErrorMessage = "Trail gap cannot be negative.")]
    //    public double trail_gap { get; set; }

    //    [Required(ErrorMessage = "Target state is required.")]
    //    public string target_state { get; set; }

    //    [Range(0, double.MaxValue, ErrorMessage = "Target trigger price cannot be negative.")]
    //    public double target_trigger_price { get; set; }

    //    [Required(ErrorMessage = "Target order price is required.")]
    //    public string target_order_price { get; set; }

    //    [Range(0, double.MaxValue, ErrorMessage = "Trail distance cannot be negative.")]
    //    public double trail_distance { get; set; }

    //    [Required(ErrorMessage = "Created at timestamp is required.")]
    //    public DateTime created_at { get; set; }

    //    [Required(ErrorMessage = "Last modified timestamp is required.")]
    //    public DateTime last_modified { get; set; }

    //    [Range(0, int.MaxValue, ErrorMessage = "GTT Order Number cannot be negative.")]
    //    public int gtt_order_no { get; set; }

    //    [Required(ErrorMessage = "Module is required.")]
    //    public string module { get; set; }

    //    [Range(0, int.MaxValue, ErrorMessage = "Filled quantity cannot be negative.")]
    //    public int filled_qty { get; set; }

    //    [Range(0, double.MaxValue, ErrorMessage = "Filled value cannot be negative.")]
    //    public double filled_value { get; set; }

    //    [Range(0, int.MaxValue, ErrorMessage = "Exit quantity cannot be negative.")]
    //    public int exit_qty { get; set; }

    //    [Range(0, double.MaxValue, ErrorMessage = "Exit value cannot be negative.")]
    //    public double exit_value { get; set; }

    //    public string reason { get; set; }

    //    [Range(0, int.MaxValue, ErrorMessage = "Flags cannot be negative.")]
    //    public int flags { get; set; }

    //    public string api_source { get; set; }

    //    [Range(int.MinValue, 0, ErrorMessage = "Sender order number must be zero or negative.")]
    //    public int sender_order_no { get; set; }

    //    // Method to validate the model before processing
    //    public void Validate()
    //    {
    //        var validationContext = new ValidationContext(this);
    //        var validationResults = new List<ValidationResult>();

    //        if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
    //        {
    //            string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
    //            throw new ApiException(HttpStatusCode.BadRequest, $"Validation failed: {errorMessages}");
    //        }
    //    }
    //}

    public class CancelGTTOrderResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public GttOrderState data { get; set; }
    }

    
}
