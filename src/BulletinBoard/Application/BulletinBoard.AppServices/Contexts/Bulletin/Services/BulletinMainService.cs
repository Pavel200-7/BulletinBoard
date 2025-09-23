using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Errors.Exeptions;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinMainService : IBulletinMainService
{
    private readonly IBulletinMainRepository _repository;
    private readonly IBulletinMainDtoValidatorFacade _validator;
    private readonly IBulletinMainSpecificationBuilder _specificationBuilder;


    /// <inheritdoc/>

    public BulletinMainService
        (
        IBulletinMainRepository repository,
        IBulletinMainDtoValidatorFacade validator,
        IBulletinMainSpecificationBuilder specificationBuilder
        )
    {
        _repository = repository;
        _validator = validator;
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    public async Task<BulletinMainDto> CreateAsync(BulletinMainCreateDto bulletinDto)
    {
        await _validator.ValidateThrowValidationExeptionAsync(bulletinDto);
        BulletinMainDto outputBulletinDto = await _repository.CreateAsync(bulletinDto);
        return outputBulletinDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinMainDto> UpdateAsync(Guid id, BulletinMainUpdateDto bulletinDto)
    {
        await _validator.ValidateThrowValidationExeptionAsync(bulletinDto);

        BulletinMainDto? outputBulletinDto = await _repository.UpdateAsync(id, bulletinDto);
        if (outputBulletinDto is null)
        {
            string errorMessage = $"The bulletin with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputBulletinDto;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _validator.ValidateBeforeDeletingThrowValidationExeptionAsync(id);
        bool isOnDeleting = await _repository.DeleteAsync(id);
        if (!isOnDeleting)
        {
            string errorMessage = $"The bulletin with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return isOnDeleting;
    }
}
