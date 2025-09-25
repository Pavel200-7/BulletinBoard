using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.UpdateDto;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator;

/// <inheritdoc/>
public class BulletinViewsCountUpdateDtoValidator : AbstractValidator<BulletinViewsCountUpdateDtoForValidating>, IBulletinViewsCountUpdateDtoValidator
{
    /// <inheritdoc/>
    public BulletinViewsCountUpdateDtoValidator()
    {
        int zero = 0;
        RuleFor(createDto => createDto.ViewsCount)
            .GreaterThanOrEqualTo(zero);
    }
}
