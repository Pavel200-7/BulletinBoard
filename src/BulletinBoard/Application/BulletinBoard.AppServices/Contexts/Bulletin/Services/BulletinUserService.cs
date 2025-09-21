using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinUserService : IBulletinUserService
{
    private readonly IBulletinUserRepository _repository;
    private readonly IBulletinUserSpecificationBuilder _specificationBuilder;
    private readonly IBulletinUserDtoValidatorFacade _validator;   


    /// <inheritdoc/>
    public BulletinUserService
        (
        IBulletinUserRepository repository,
        IBulletinUserSpecificationBuilder specificationBuilder,
        IBulletinUserDtoValidatorFacade validator
        )
    {
        _repository = repository;
        _specificationBuilder = specificationBuilder;
        _validator = validator;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> GetByIdAsync(Guid id)
    {
        BulletinUserDto? outputUserDto = await _repository.GetByIdAsync(id);
        if (outputUserDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputUserDto;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinUserDto>> GetAsync(BulletinUserFilterDto userFilterDto)
    {
        if (userFilterDto.IsUsedFullName)
        {
            _specificationBuilder.WhereFullName(userFilterDto.FullName);
        } 
        else if (userFilterDto.IsUsedFullNameContains)
        {
            _specificationBuilder.WhereFullNameContains(userFilterDto.FullName);
        }

        if (userFilterDto.IsUsedBlocked)
        {
            _specificationBuilder.WhereIsBlocked(userFilterDto.Blocked);
        }

        if (userFilterDto.IsUsedCoordinates)
        {
            if (userFilterDto.IsUsedCoordinatesEquals)
            {
                _specificationBuilder.WhereCoordinates(userFilterDto.Latitude, userFilterDto.Longitude);
            }
            else if (userFilterDto.IsUsedCoordinatesCloser)
            {
                _specificationBuilder.WhereCoordinatesCloser(userFilterDto.Latitude, userFilterDto.Longitude, userFilterDto.Distance);
            }
            else if (userFilterDto.IsUsedCoordinatesFarther)
            {
                _specificationBuilder.WhereCoordinatesFarther(userFilterDto.Latitude, userFilterDto.Longitude, userFilterDto.Distance);
            }
        }

        if (userFilterDto.IsUsedFormattedAddress)
        {
            _specificationBuilder.WhereFormattedAddress(userFilterDto.FormattedAddress);
        }

        if (userFilterDto.IsUsedPhone)
        {
            _specificationBuilder.WherePhone(userFilterDto.Phone);
        }

        if (userFilterDto.IsUsedCoordinates)
        {
            switch (true)
            {
                case true when userFilterDto.IsUsedCoordinatesEquals == true:
                    _specificationBuilder.WhereCoordinates(userFilterDto.Latitude, userFilterDto.Longitude);
                    break;
                case true when userFilterDto.IsUsedCoordinatesCloser == true:
                    _specificationBuilder.WhereCoordinatesCloser(userFilterDto.Latitude, userFilterDto.Longitude, userFilterDto.Distance);
                    break;
                case true when userFilterDto.IsUsedCoordinatesFarther == true:
                    _specificationBuilder.WhereCoordinatesFarther(userFilterDto.Latitude, userFilterDto.Longitude, userFilterDto.Distance);
                    break;
            }
        }

        ExtendedSpecification<BulletinUser> specification = _specificationBuilder
            .Build();

        IReadOnlyCollection<BulletinUserDto> userDtoCollection = await _repository.FindAsync(specification);

        return userDtoCollection;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> CreateAsync(BulletinUserCreateDto userDto)
    {
        BulletinUserDto outputUserDto = await _repository.CreateAsync(userDto);
        return outputUserDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangeNameAsync(Guid id, string name)
    {
        BulletinUserDto? userDto = await _repository.ChangeNameAsync(id, name);

        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return userDto;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        ValidationResult validationResult = await _validator.ValidateBeforeDeletingAsync(id);
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }

        bool isOnDeleting = await _repository.DeleteAsync(id);

        if (!isOnDeleting)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return isOnDeleting;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangeAdressAsync(Guid id, BulletinUserUpdateLocationDto userLocationDto)
    {
        BulletinUserDto? userDto = await _repository.ChangeAdressAsync(id, userLocationDto);
        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangePhoneAsync(Guid id, string phone)
    {
        BulletinUserDto? userDto = await _repository.ChangePhoneAsync(id, phone);
        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> BlockUserAsync(Guid id)
    {
        BulletinUserDto? userDto;
        userDto = await _repository.GetByIdAsync(id);
        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto = await _repository.SetUserBlockStatusAsync(id, true);

        return userDto!;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> UnBlockUserAsync(Guid id)
    {
        BulletinUserDto? userDto;
        userDto = await _repository.GetByIdAsync(id);
        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto = await _repository.SetUserBlockStatusAsync(id, false);

        return userDto!;
    }
}
