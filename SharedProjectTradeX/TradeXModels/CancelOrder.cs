using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;


namespace TradeX
{
    public class CancelOrderRequest
    {
        [Required(ErrorMessage = "Client is required.")]
        public string client { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        public string code { get; set; }

        [Required(ErrorMessage = "Exchange is required.")]
        [EnumDataType(typeof(ExchangeType), ErrorMessage = "Invalid Exchange Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExchangeType exchange { get; set; }

        [Required(ErrorMessage = "Exchange Order Number is required.")]
        public string exchange_order_no { get; set; }

        [Required(ErrorMessage = "User Order No is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User Order No must be greater than zero.")]
        public int user_order_no { get; set; }

        [Required(ErrorMessage = "Sender Order No is required.")]
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

    public class CancelOrderResponseWrapper
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public CancelOrderResponse data { get; set; }
    }
    public class CancelOrderResponse
    {
        public string exchange_order_no { get; set; }
        public Int32 user_order_no { get; set; }
        public Int32 sender_order_no { get; set; }
        public string client { get; set; }

    }
}
