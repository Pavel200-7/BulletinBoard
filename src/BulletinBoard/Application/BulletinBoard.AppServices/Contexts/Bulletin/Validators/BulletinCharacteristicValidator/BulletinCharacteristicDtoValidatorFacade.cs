using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator;

/// <inheritdoc/>
public class BulletinCharacteristicDtoValidatorFacade : IBulletinCharacteristicDtoValidatorFacade
{
    private readonly IBulletinCharacteristicCreateDtoValidator _createDtoValidator;
    private readonly IBulletinCharacteristicUpdateDtoValidator _updateDtoValidator;

    /// <inheritdoc/>
    public BulletinCharacteristicDtoValidatorFacade
        (
            IBulletinCharacteristicCreateDtoValidator bulletinCharacteristicCreateDto,
            IBulletinCharacteristicUpdateDtoValidator bulletinCharacteristicUpdateDto
        )
    {
        _createDtoValidator = bulletinCharacteristicCreateDto;
        _updateDtoValidator = bulletinCharacteristicUpdateDto;
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinCharacteristicCreateDto entityDto)
    {
        return await _createDtoValidator.ValidateAsync(entityDto);
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinCharacteristicUpdateDtoForValidating entityDto)
    {
        return await _updateDtoValidator.ValidateAsync(entityDto);
    }
}
