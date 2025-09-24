using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.CommonValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        RuleFor(createDto => createDto.Name)
            .NotEmpty()
            .Length(3, 255)
            .Matches("^[а-яА-Яa-zA-Z0-9\\s.,'-]+$")
                .WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z), digits, spaces, and some punctuation");
    }
}
