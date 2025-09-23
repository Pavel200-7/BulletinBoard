using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Errors.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinRatingService : IBulletinRatingService
{
    private readonly IBulletinRatingRepository _repository;
    private readonly IBulletinRatingSpecificationBuilder _specificationBuilder;
    private readonly IBulletinRatingDtoValidatorFacade _validator;


    /// <inheritdoc/>
    public BulletinRatingService
        (
        IBulletinRatingRepository repository,
        IBulletinRatingSpecificationBuilder specificationBuilder,
        IBulletinRatingDtoValidatorFacade validator
        )
    {
        _repository = repository;
        _specificationBuilder = specificationBuilder;
        _validator = validator;
    }

    /// <inheritdoc/>
    public async Task<BulletinRatingDto> GetByIdAsync(Guid id)
    {
        BulletinRatingDto? outputRatingDto = await _repository.GetByIdAsync(id);
        if (outputRatingDto is null)
        {
            string errorMessage = $"The rating with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputRatingDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinRatingDto> CreateAsync(BulletinRatingCreateDto ratingDto)
    {
        await _validator.ValidateThrowValidationExeptionAsync(ratingDto);
        BulletinRatingDto outputRatingDto = await _repository.CreateAsync(ratingDto);
        return outputRatingDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinRatingDto> UpdateAsync(Guid id, BulletinRatingUpdateDto ratingDto)
    {
        await _validator.ValidateThrowValidationExeptionAsync(ratingDto);

        BulletinRatingDto? outputRatingDto = await _repository.UpdateAsync(id, ratingDto);
        if (outputRatingDto is null)
        {
            string errorMessage = $"The rating with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputRatingDto;

    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _validator.ValidateBeforeDeletingThrowValidationExeptionAsync(id);
        bool isOnDeleting = await _repository.DeleteAsync(id);
        if (!isOnDeleting)
        {
            string errorMessage = $"The rating with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return isOnDeleting;
    }
}
