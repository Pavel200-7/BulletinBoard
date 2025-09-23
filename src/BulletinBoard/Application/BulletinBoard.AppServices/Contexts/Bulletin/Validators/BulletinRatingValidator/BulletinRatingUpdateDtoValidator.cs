using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator;

/// <inheritdoc/>
public class BulletinRatingUpdateDtoValidator : AbstractValidator<BulletinRatingUpdateDto>, IBulletinRatingUpdateDtoValidator
{
    /// <inheritdoc/>
    public BulletinRatingUpdateDtoValidator()
    {
        RuleFor(bulletinRatingUpdateDto => bulletinRatingUpdateDto.Rating)
            .NotNull().WithMessage("Rating can not be null.")
            .InclusiveBetween(1, 10).WithMessage("Rating must be in 1 and 10.");
    }
}
