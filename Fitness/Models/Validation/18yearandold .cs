using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Fitness.Models.Viewmodel;


namespace Fitness.Models.Validation
{
    public class _18yearandold : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Customer = (Signuptrainerviewmodel)validationContext.ObjectInstance;
            if (Customer.DateOfBirth == null)
            {
                return new ValidationResult("Birthdate is require");

            }
            var age = DateTime.Today.Year - Customer.DateOfBirth.Year;
            if (age >= 18)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("User must be 18 year and older");
            }
        }
    }
}