using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;


namespace TradeX
{
    public class CancelAllOrderRequest
    {
        [Required(ErrorMessage = "Client is required.")]
        public string client { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Code must be greater than zero.")]
        public int code { get; set; }

        [Required(ErrorMessage = "Exchange is required.")]
        [EnumDataType(typeof(ExchangeType), ErrorMessage = "Invalid Exchange Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExchangeType exchange { get; set; }

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

    public class CancelAllOrderResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
