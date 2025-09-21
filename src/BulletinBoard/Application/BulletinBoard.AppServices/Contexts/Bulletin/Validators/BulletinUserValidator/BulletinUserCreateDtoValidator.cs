using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator;

/// <inheritdoc/>
public class BulletinUserCreateDtoValidator : AbstractValidator<BulletinUserCreateDto>, IBulletinUserCreateDtoValidator
{
    /// <inheritdoc/>
    public BulletinUserCreateDtoValidator()
    {
        // Валидации нет. (не предполагается текущими требованиями)
    }
}
