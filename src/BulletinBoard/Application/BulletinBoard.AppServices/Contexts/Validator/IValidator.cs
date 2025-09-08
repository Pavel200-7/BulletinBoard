using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Validator;

public interface IValidator<T>
{
    public Task<ValidationResult> ValidateAsync(T entityDto, CancellationToken cancellation = new());
}
