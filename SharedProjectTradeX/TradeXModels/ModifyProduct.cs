using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace TradeX
{
    public class ModifyProductRequest
    {
        [Required(ErrorMessage = "Client is required.")]
        public string client { get; set; }

        [Required(ErrorMessage = "Exchange is required.")]
        [EnumDataType(typeof(ExchangeType), ErrorMessage = "Invalid Exchange Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExchangeType exchange { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        public string code { get; set; }

        [Required(ErrorMessage = "Old Product is required.")]
        [EnumDataType(typeof(ProductType), ErrorMessage = "Invalid Product Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType old_product { get; set; }

        [Required(ErrorMessage = "New Product is required.")]
        [EnumDataType(typeof(ProductType), ErrorMessage = "Invalid Product Type.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType new_product { get; set; }

        [Required(ErrorMessage = "Side is required.")]
        [EnumDataType(typeof(SideType), ErrorMessage = "Invalid Side Type. Only 'Buy' or 'Sell' is allowed.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SideType side { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int qty { get; set; }

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

    public class ProductModificationResponseWrapper
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public ProductModificationResponse data { get; set; }
    }

    public class ProductModificationResponse
    {
        public string status { get; set; }
        public Int32 user_order_no { get; set; }
        public string message { get; set; }
    }
}
