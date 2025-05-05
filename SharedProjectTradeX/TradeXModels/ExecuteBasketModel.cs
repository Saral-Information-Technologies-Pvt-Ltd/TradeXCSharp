using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;


namespace TradeX
{
    public class ExecuteBasketRequest
    {
        [Required(ErrorMessage = "Client is required.")]
        public string client { get; set; }

        [Required(ErrorMessage = "Orders list is required.")]
        [MinLength(1, ErrorMessage = "At least one order must be present.")]
        public List<ExecuteBasketRequestOrder> orders { get; set; }

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

            foreach (var order in orders)
            {
                order.Validate();
            }
        }
    }

    public class ExecuteBasketRequestOrder
    {
        [Required(ErrorMessage = "Exchange is required.")]
        [EnumDataType(typeof(ExchangeType), ErrorMessage = "Invalid Exchange Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExchangeType exchange { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        public string code { get; set; }

        [Required(ErrorMessage = "Side is required.")]
        [EnumDataType(typeof(SideType), ErrorMessage = "Invalid SideType Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SideType side { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price cannot be negative.")]
        public int price { get; set; }

        [Required(ErrorMessage = "Client is required.")]
        public string client { get; set; }

        [Required(ErrorMessage = "Book is required.")]
        [EnumDataType(typeof(BookType), ErrorMessage = "Invalid Book Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BookType book { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Trigger price cannot be negative.")]
        public int trigger_price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Disclosed quantity cannot be negative.")]
        public int disclosed_qty { get; set; }

        [Required(ErrorMessage = "Product is required.")]
        [EnumDataType(typeof(ProductType), ErrorMessage = "Invalid Product Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType product { get; set; }

        [Required(ErrorMessage = "Validity is required.")]
        [EnumDataType(typeof(ValidityType), ErrorMessage = "Invalid Validity Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ValidityType validity { get; set; }

        public string gtd { get; set; } // Can be null

        public int order_flag { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Sender Order cannot be negative.")]
        public int sender_order_no { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Algol ID cannot be negative.")]
        public int algol_id { get; set; }

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

    public class ExecuteBasketResponse
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

}

