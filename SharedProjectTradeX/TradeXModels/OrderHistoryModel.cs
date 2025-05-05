using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace TradeX
{
    public class OrderHistoryRequest
    {
        [Required(ErrorMessage = "Client is required.")]
        public string client { get; set; }

        [Required(ErrorMessage = "Exchange is required.")]
        [EnumDataType(typeof(ExchangeType), ErrorMessage = "Invalid Exchange Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExchangeType exchange { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        public string code { get; set; }

        [Required(ErrorMessage = "Exchange Order Number is required.")]
        public string exchange_order_no { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Sender Order cannot be negative.")]
        public int sender_order_no { get; set; }

        /// <summary>
        /// Validates the model and throws an ApiException if validation fails.
        /// </summary>
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

    public class OrderHistoryResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public List<OrderState> data { get; set; }
    }
}
