using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.IValidator
{
    public interface IValidator<T>
    {
        public Task<ValidationResult> ValidateAsync(T entityDto, CancellationToken cancellation = new());
    }
}
