using System.ComponentModel.DataAnnotations;

namespace AspDotNetCoreMVCProject.Models.Custom_Validation_Attribute
{
    public class ValidAgeRange : ValidationAttribute
    {
        private readonly int _minAge;
        private readonly int _maxAge;
        public ValidAgeRange(int minAge, int maxAge)
        {
            _minAge = minAge;
            _maxAge = maxAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please provide your Age.");
            }
            if (value is int Age)
            {
                if (Age >= _minAge && Age <= _maxAge)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"Age Must be between: {_minAge} - {_maxAge}");
                }
            }

            return new ValidationResult($"Age must be integer number.");
        }
    }
}
