using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.CommonValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain.CreateDto;
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

        RuleFor(createDto => createDto.UserId)
            .SetAsyncValidator(new UserIdValidator<BulletinMainCreateDto>(_userRepository));

        RuleFor(createDto => createDto.Title)
            .NotEmpty()
            .Length(3, 100)
            .Matches("^[а-яА-Яa-zA-Z0-9\\s.,'-]+$")
                .WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z), digits, spaces, and some punctuation")
            .SetAsyncValidator(new BulletinTitleValudator<BulletinMainCreateDto>(_bulletinRepository, _mainSpecificationBuilder));

        RuleFor(createDto => createDto.Description)
            .NotEmpty()
            .Length(3, 1000)
            .Matches("^[а-яА-Яa-zA-Z0-9\\s.,'-]+$")
                .WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z), digits, spaces, and some punctuation");

        RuleFor(createDto => createDto.CategoryId)
            .NotEmpty()
            .SetAsyncValidator(new BulletinCategoryValidator<BulletinMainCreateDto>(_categoryRepository));

        RuleFor(createDto => createDto.Price)
            .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be a non-negative number");
    }
}
