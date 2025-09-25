using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.CommonValidators;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator;

/// <inheritdoc/>
public class BulletinImageCreateDtoValidator : AbstractValidator<BulletinImageCreateDto>, IBulletinImageCreateDtoValidator
{
    private readonly IBulletinMainRepository _bulletibRepository;

    /// <inheritdoc/>
    public BulletinImageCreateDtoValidator
        (
        IBulletinMainRepository bulletibRepository
        )
    {
        _bulletibRepository = bulletibRepository;

        RuleFor(createDto => createDto.Id)
            .NotEmpty();

        RuleFor(createDto => createDto.BulletinId)
            .SetAsyncValidator(new BulletinIdValidator<BulletinImageCreateDto>(_bulletibRepository));
    }
}
