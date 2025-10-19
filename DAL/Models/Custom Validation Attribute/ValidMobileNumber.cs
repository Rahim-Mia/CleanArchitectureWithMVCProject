using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AspDotNetCoreMVCProject.Models.Custom_Validation_Attribute
{
    public class ValidMobileNumber : ValidationAttribute
    {
        private const string pattern = @"^01[5-9]\d{8}$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Mobile number is Required.");
            }
            string mobile = value.ToString();
            if (!Regex.IsMatch(mobile, pattern))
            {
                return new ValidationResult("Enter a valid 11 digit mobile number.");
            }
            return ValidationResult.Success;
        }
    }
}
