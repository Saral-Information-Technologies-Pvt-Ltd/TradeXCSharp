using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace TradeX
{
    public class ModifyOrderRequest
    {
        [Required(ErrorMessage = "Exchange is required.")]
        [EnumDataType(typeof(ExchangeType), ErrorMessage = "Invalid Exchange Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExchangeType exchange { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        public string code { get; set; }

        [Required(ErrorMessage = "Exchange Order Number is required.")]
        public string exchange_order_no { get; set; }

        [Required(ErrorMessage = "Side is required.")]
        [EnumDataType(typeof(SideType), ErrorMessage = "Invalid Side Type. Only 'Buy' or 'Sell' is allowed.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SideType side { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
        public double price { get; set; }

        [Required(ErrorMessage = "Client is required.")]
        public string client { get; set; }

        [Required(ErrorMessage = "Book is required.")]
        [EnumDataType(typeof(BookType), ErrorMessage = "Invalid Book Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BookType book { get; set; }

        [Required(ErrorMessage = "Trigger Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Trigger Price cannot be negative.")]
        public double trigger_price { get; set; }

        [Required(ErrorMessage = "Disclosed Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Disclosed Quantity cannot be negative.")]
        public int disclosed_qty { get; set; }

        [Required(ErrorMessage = "Product is required.")]
        [EnumDataType(typeof(ProductType), ErrorMessage = "Invalid Product Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType product { get; set; }

        [Required(ErrorMessage = "Validity is required.")]
        [EnumDataType(typeof(ValidityType), ErrorMessage = "Invalid Validity Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ValidityType validity { get; set; }

        public string gtd { get; set; } // GTD is optional

        [Required(ErrorMessage = "Order Flag is required.")]
        public int order_flag { get; set; }

        [Required(ErrorMessage = "Sender Order No is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Sender Order cannot be negative.")]
        public int sender_order_no { get; set; }

        [Required(ErrorMessage = "Remaining Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Remaining Quantity cannot be negative.")]
        public int qty_remaining { get; set; }

        [Required(ErrorMessage = "Traded Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Traded Quantity cannot be negative.")]
        public int qty_traded { get; set; }

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




    public class ModifyOrderResponseWrapper
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public ModifyOrderResponse data { get; set; }
    }
        public class ModifyOrderResponse
        {
        public string exchange_order_no { get; set; }
        public Int32 user_order_no { get; set; }
        public Int32 sender_order_no { get; set; }
        public string client { get; set; }
}
    }

