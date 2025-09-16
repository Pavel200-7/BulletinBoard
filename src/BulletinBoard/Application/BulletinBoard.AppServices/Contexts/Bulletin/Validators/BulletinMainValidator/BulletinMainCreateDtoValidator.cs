using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator;

/// <inheritdoc/>
public class BulletinMainCreateDtoValidator : AbstractValidator<BulletinMainCreateDto>, IBulletinMainCreateDtoValidator
{
    private readonly IBulletinUserRepository _userRepository;
    private readonly IBulletinMainRepository _bulletinRepository;
    private readonly IBulletinCategoryRepository _categoryRepository;


    private readonly IBulletinMainSpecificationBuilder _mainSpecificationBuilder;

    /// <inheritdoc/>
    public BulletinMainCreateDtoValidator
        (
            IBulletinUserRepository userRepository,
            IBulletinMainRepository bulletinRepository,
            IBulletinCategoryRepository bulletinCategory,
            IBulletinMainSpecificationBuilder mainSpecificationBuilder
        )
    {
        _userRepository = userRepository;
        _bulletinRepository = bulletinRepository;
        _mainSpecificationBuilder = mainSpecificationBuilder;
        _categoryRepository = bulletinCategory;

        RuleFor(bulletinMainCreateDto => bulletinMainCreateDto.BulletinUserId)
            .SetAsyncValidator(new UserIdValidator<BulletinMainCreateDto>(_userRepository));

        RuleFor(bulletinMainCreateDto => bulletinMainCreateDto.Title)
            .NotEmpty()
            .Length(3, 100)
            .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces")
            .SetAsyncValidator(new BulletinTitleValudator<BulletinMainCreateDto>(_bulletinRepository, _mainSpecificationBuilder));

        RuleFor(bulletinMainCreateDto => bulletinMainCreateDto.Description)
            .NotEmpty()
            .Length(3, 1000)
            .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces");

        RuleFor(bulletinMainCreateDto => bulletinMainCreateDto.CategoryId)
            .NotEmpty()
            .SetAsyncValidator(new BulletinCategoryValidator<BulletinMainCreateDto>(_categoryRepository));

        RuleFor(bulletinMainCreateDto => bulletinMainCreateDto.Price)
            .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be a non-negative number");
    }
}
