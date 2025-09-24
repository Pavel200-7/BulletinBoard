using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator;

/// <inheritdoc/>
public class BulletinViewsCountDeleteValidator : AbstractValidator<Guid>, IBulletinViewsCountDeleteValidator
{
    /// <inheritdoc/>
    public BulletinViewsCountDeleteValidator()
    {
        // Валидации нет. (не предполагается текущими требованиями)
    }
}
