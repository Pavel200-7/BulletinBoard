using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator;

/// <inheritdoc/>
public class BulletinRatingDeleteValidator : AbstractValidator<Guid>, IBulletinRatingDeleteValidator
{
    /// <inheritdoc/>
    public BulletinRatingDeleteValidator()
    {
        // Валидации нет. (не предполагается текущими требованиями)
    }
}

