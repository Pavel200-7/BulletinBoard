using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Validator;

public interface IValidatorFacede<T>
{
    public Task<ValidationResult> ValidateAsync(T entityDto);
}
