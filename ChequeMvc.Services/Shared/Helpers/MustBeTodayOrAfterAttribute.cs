using System;
using System.ComponentModel.DataAnnotations;

namespace ChequeMvc.Services.Shared.Helpers
{
    /// <summary>
    /// Custom attribute to make sure that the cheque date entered
    /// is set today or later.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeTodayOrAfterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = Convert.ToDateTime(value);
            if (dt >= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
