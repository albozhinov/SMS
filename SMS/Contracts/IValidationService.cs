namespace SMS.Contracts
{
using SMS.ViewModels;

    public interface IValidationService
    {
        (bool isValid, string error) ValidateModel(object model);

        (bool isValid, string error) NullOrWhiteSpacesCheck(RegisterViewModel model);
    }
}
