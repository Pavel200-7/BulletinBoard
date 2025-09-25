using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinImage.UpdateDto;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator;

/// <inheritdoc/>
public class BulletinImageUpdateDtoValidator : AbstractValidator<BulletinImageUpdateDtoForValidating>, IBulletinImageUpdateDtoValidator
{
    /// <inheritdoc/>
    public BulletinImageUpdateDtoValidator()
    {
        // нет требований.

    }
}
