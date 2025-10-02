using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.UpdateDto;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator;

/// <inheritdoc/>
public class BulletinDtoValidatorFacade : BaseValidatorFacade
    <
    BelletinCreateDto,
    BelletinUpdateDtoForValidating,
    IBulletinCreateDtoValidator,
    IBulletinUpdateDtoValidator,
    IBulletinDeleteValidator
    >, IBulletinDtoValidatorFacade
{
    private readonly IBulletinPaginationRequestDtoValidator _paginationRequestDtoValidator;

    /// <inheritdoc/>
    public BulletinDtoValidatorFacade
        (
        IBulletinCreateDtoValidator bulletinCategoryCreateDtoValidator,
        IBulletinUpdateDtoValidator bulletinCategoryUpdateDtoValidator,
        IBulletinDeleteValidator deleteValidator,
        IBulletinPaginationRequestDtoValidator paginationRequestDtoValidator
        ) : base(bulletinCategoryCreateDtoValidator, bulletinCategoryUpdateDtoValidator, deleteValidator)
    {
        _paginationRequestDtoValidator = paginationRequestDtoValidator;
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinPaginationRequestDto requersDto)
    {
        return await _paginationRequestDtoValidator.ValidateAsync(requersDto);
    }

    /// <inheritdoc/>
    public async Task ValidateThrowValidationExeptionAsync(BulletinPaginationRequestDto requersDto)
    {
        var validationResult = await _paginationRequestDtoValidator.ValidateAsync(requersDto);
        CheckValidationResult(validationResult);
    }
}