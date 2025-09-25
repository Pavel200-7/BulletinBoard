using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.CommonValidators;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator;

/// <inheritdoc/>
public class BulletinViewsCountCreateDtoValidator : AbstractValidator<BulletinViewsCountCreateDto>, IBulletinViewsCountCreateDtoValidator
{
    private readonly IBulletinMainRepository _bulletibRepository;

    /// <inheritdoc/>
    public BulletinViewsCountCreateDtoValidator
        (
        IBulletinMainRepository bulletibRepository
        )
    {
        _bulletibRepository = bulletibRepository;

        RuleFor(createDto => createDto.BulletinId)
            .SetAsyncValidator(new BulletinIdValidator<BulletinViewsCountCreateDto>(_bulletibRepository));

        int zero = 0;
        RuleFor(createDto => createDto.ViewsCount)
            .Equal(zero);
    }
}
