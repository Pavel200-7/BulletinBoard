using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.CommonValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator;

/// <inheritdoc/>
public class BulletinRatingCreateDtoValidator : AbstractValidator<BulletinRatingCreateDto>, IBulletinRatingCreateDtoValidator
{
    private readonly IBulletinUserRepository _userRepository;
    private readonly IBulletinMainRepository _bulletibRepository;

    /// <inheritdoc/>
    public BulletinRatingCreateDtoValidator
        (
        IBulletinUserRepository userRepository,
        IBulletinMainRepository bulletibRepository
        )
    {
        _userRepository = userRepository;
        _bulletibRepository = bulletibRepository;

        RuleFor(createDto => createDto.UserId)
            .SetAsyncValidator(new UserIdValidator<BulletinRatingCreateDto>(_userRepository));

        RuleFor(createDto => createDto.BulletinId)
            .SetAsyncValidator(new BulletinIdValidator<BulletinRatingCreateDto>(_bulletibRepository));

        RuleFor(createDto => createDto.Rating)
            .NotNull().WithMessage("Rating не может быть null.")
            .InclusiveBetween(1, 10).WithMessage("Rating должен быть целым числом в диапазоне от 1 до 10.");
    }
}
