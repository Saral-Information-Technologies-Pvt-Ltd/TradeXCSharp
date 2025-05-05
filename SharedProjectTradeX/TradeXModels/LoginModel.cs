using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "App Key is required.")]
        public string app_key { get; set; }

        [Required(ErrorMessage = "Secret Key is required.")]
        public string secret_key { get; set; }

        [Required(ErrorMessage = "Source is required.")]
        public string source { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "User ID must be between 4 and 10 digits.")]
        public string user_id { get; set; }

        // Method to validate the model
        public void Validate()
        {
            var validationContext = new ValidationContext(this);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ApiException(System.Net.HttpStatusCode.BadRequest, $"Validation failed: {errorMessages}");
            }
        }
    }

    public class LoginResponseWrapper
    {
        public Int32 status { get; set; }
        public string message { get; set; }
        public LoginResponse data { get; set; }
    }

    public class LoginResponse
    {
        public string user_id { get; set; }
        public string exchanges_allowed { get; set; }
        public string products_allowed { get; set; }
        public string token { get; set; }
    }
}


