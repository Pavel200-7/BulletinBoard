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
    private readonly IBulletinMainRepository _bulletinRepository;
    private readonly IBulletinMainDtoValidatorFacade _validator;

    /// <inheritdoc/>

    public BulletinMainService(
        IBulletinMainRepository bulletinRepository,
        IBulletinMainDtoValidatorFacade validator
        )
    {
        _bulletinRepository = bulletinRepository;
        _validator = validator;
    }

    /// <inheritdoc/>
    public async Task<BulletinMainDto> CreateAsync(BulletinMainCreateDto bulletinDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(bulletinDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }

        BulletinMainDto outputBulletinDto = await _bulletinRepository.CreateAsync(bulletinDto);

        return outputBulletinDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinMainDto> UpdateAsync(Guid id, BulletinMainUpdateDto bulletinDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(bulletinDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }

        BulletinMainDto? outputBulletinDto = await _bulletinRepository.UpdateAsync(id, bulletinDto);

        if (outputBulletinDto is null)
        {
            string errorMessage = $"The category with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputBulletinDto;

    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        bool isOnDeleting = await _bulletinRepository.DeleteAsync(id);

        if (!isOnDeleting)
        {
            string errorMessage = $"The bulletin with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return isOnDeleting;
    }
}
