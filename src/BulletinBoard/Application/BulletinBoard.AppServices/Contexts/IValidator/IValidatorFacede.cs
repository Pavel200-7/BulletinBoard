using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.IValidator
{
    public interface IValidatorFacede<T>
    {
        public Task<ValidationResult> ValidateAsync(T entityDto);
    }
}
