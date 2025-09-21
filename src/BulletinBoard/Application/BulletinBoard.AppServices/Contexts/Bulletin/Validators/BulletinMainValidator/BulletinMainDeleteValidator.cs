using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator;

/// <inheritdoc/>
public class BulletinMainDeleteValidator : AbstractValidator<Guid>, IBulletinMainDeleteValidator
{
    /// <inheritdoc/>
    public BulletinMainDeleteValidator()
    {
        // Валидации нет. (не предполагается текущими требованиями)
    }
}
