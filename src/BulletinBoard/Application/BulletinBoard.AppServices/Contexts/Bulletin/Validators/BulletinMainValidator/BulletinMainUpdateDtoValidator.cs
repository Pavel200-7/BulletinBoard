using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BelletinMain.UpdateDto;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator;

/// <inheritdoc/>
public class BulletinMainUpdateDtoValidator : AbstractValidator<BulletinMainUpdateDtoForValidating>, IBulletinMainUpdateDtoValidator
{
    private readonly IBulletinMainRepository _bulletinRepository;

    private readonly IBulletinMainSpecificationBuilder _mainSpecificationBuilder;

    /// <inheritdoc/>
    public BulletinMainUpdateDtoValidator
        (
            IBulletinMainRepository bulletinRepository,
            IBulletinMainSpecificationBuilder mainSpecificationBuilder
        )
    {
        _bulletinRepository = bulletinRepository;
        _mainSpecificationBuilder = mainSpecificationBuilder;

        RuleFor(updateDto => updateDto.Title)
            .NotEmpty()
            .Length(3, 100)
            .Matches("^[а-яА-Яa-zA-Z0-9\\s.,'-]+$")
                .WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z), digits, spaces, and some punctuation");

        RuleFor(updateDto => updateDto.Description)
            .NotEmpty()
            .Length(3, 1000)
            .Matches("^[а-яА-Яa-zA-Z0-9\\s.,'-]+$")
                .WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z), digits, spaces, and some punctuation");

        RuleFor(updateDto => updateDto.Price)
            .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be a non-negative number");
    }
}
