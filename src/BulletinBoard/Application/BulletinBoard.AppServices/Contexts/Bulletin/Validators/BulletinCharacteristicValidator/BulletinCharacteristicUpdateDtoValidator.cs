using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator;

/// <inheritdoc/>
public class BulletinCharacteristicUpdateDtoValidator : AbstractValidator<BulletinCharacteristicUpdateDtoForValidating>, IBulletinCharacteristicUpdateDtoValidator
{
    private readonly IBulletinCharacteristicRepository _characteristicRepository;
    private readonly IBulletinCharacteristicSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicUpdateDtoValidator
        (
            IBulletinCharacteristicRepository characteristicRepository,
            IBulletinCharacteristicSpecificationBuilder specificationBuilder
        )
    {
        _characteristicRepository = characteristicRepository;
        _specificationBuilder = specificationBuilder;

        RuleFor(updateDto => updateDto.Name)
            .NotEmpty()
            .Length(3, 35)
            .Matches("^[а-яА-Яa-zA-Z0-9\\s]+$")
                .WithMessage("{PropertyName} can contain only letters, digits, and spaces")
            .MustAsync(async (dto, name, cancellation) =>
                await BullerinCharacteristicNameValidator.IsNameUniqueForCategoryAsync(
                    _characteristicRepository,
                    _specificationBuilder,
                    dto.CategoryId,
                    name))
                .WithMessage("This name is not unique for this category.");
    }
}

