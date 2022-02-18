namespace SMS.Services
{
    using SMS.Contracts;
    using SMS.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ValidationService : IValidationService
    {
        public (bool isValid, string error) NullOrWhiteSpacesCheck(RegisterViewModel model)
        {
            bool isValid = true;
            string error = null;

            var neshto = model.Email.Any(char.IsWhiteSpace);

            if (string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Username) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                model.Email.Contains(' ') ||
                model.Username.Contains(' ') ||
                model.Password.Contains(' '))
            {
                error = "Please set correct value.";
                isValid = false;

                return (isValid, error);
            }

            return (isValid, error);
        }

        public (bool isValid, string error) ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var errorResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(model, context, errorResult, true);

            if (isValid)
            {
                return (isValid, null);
            }

            string error = String.Join(", ", errorResult.Select(e => e.ErrorMessage));

            return (isValid, error);
        }
    }
}
