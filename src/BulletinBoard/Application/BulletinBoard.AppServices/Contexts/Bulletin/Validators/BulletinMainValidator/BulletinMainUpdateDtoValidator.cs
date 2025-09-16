using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator;

/// <inheritdoc/>
public class BulletinMainUpdateDtoValidator : AbstractValidator<BulletinMainUpdateDto>, IBulletinMainUpdateDtoValidator
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

        RuleFor(bulletinMainUpdateDto => bulletinMainUpdateDto.Title)
            .NotEmpty()
            .Length(3, 100)
            .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces")
            .SetAsyncValidator(new BulletinTitleValudator<BulletinMainUpdateDto>(_bulletinRepository, _mainSpecificationBuilder));

        RuleFor(bulletinMainUpdateDto => bulletinMainUpdateDto.Description)
            .NotEmpty()
            .Length(3, 1000)
            .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces");

        RuleFor(bulletinMainUpdateDto => bulletinMainUpdateDto.Price)
            .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be a non-negative number");
    }
}
