using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinImage.ForValidating;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator;

/// <inheritdoc/>
public class BulletinImageUpdateDtoValidator : AbstractValidator<BulletinImageUpdateDtoForValidating>, IBulletinImageUpdateDtoValidator
{
    /// <inheritdoc/>
    public BulletinImageUpdateDtoValidator()
    {
        RuleFor(updateDto => updateDto.Name)
            .NotEmpty()
            .Length(3, 255)
            .Matches("^[а-яА-Яa-zA-Z0-9\\s.,'-]+$")
                .WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z), digits, spaces, and some punctuation");
    }
}
