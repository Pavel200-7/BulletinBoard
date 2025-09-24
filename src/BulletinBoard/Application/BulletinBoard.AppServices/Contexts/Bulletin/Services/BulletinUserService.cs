using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinRating.ForValidating;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinUser.ForValidating;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation.Results;
using System.Threading;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinUserService : BaseCRUDService
    <
    BulletinUserDto,
    BulletinUserCreateDto,
    BulletinUserUpdateDto,
    BulletinUserUpdateDtoForValidating,
    IBulletinUserRepository,
    IBulletinUserDtoValidatorFacade
    >, IBulletinUserService
{
    /// <inheritdoc/>
    protected override string EntityName { get; } = "user";

    private readonly IBulletinUserSpecificationBuilder _specificationBuilder;


    /// <inheritdoc/>
    public BulletinUserService
        (
        IBulletinUserRepository repository,
        IBulletinUserDtoValidatorFacade validator,
        IMapper automapper,
        IBulletinUserSpecificationBuilder specificationBuilder
        ) : base(repository, validator, automapper)
    {
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    protected override Task<BulletinUserUpdateDtoForValidating> GetUpdateValidationDto(Guid id, BulletinUserUpdateDto updateDto)
    {
        var validatinoDto = _autoMapper.Map<BulletinUserUpdateDtoForValidating>(updateDto);
        return Task.FromResult(validatinoDto);
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
    public async Task<BulletinUserDto> ChangeNameAsync(Guid id, string name, CancellationToken cancellationToken)
    {
        BulletinUserDto? userDto = await _repository.ChangeNameAsync(id, name, cancellationToken);

        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangeAdressAsync(Guid id, BulletinUserUpdateLocationDto userLocationDto, CancellationToken cancellationToken)
    {
        BulletinUserDto? userDto = await _repository.ChangeAdressAsync(id, userLocationDto, cancellationToken);
        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangePhoneAsync(Guid id, string phone, CancellationToken cancellationToken)
    {
        BulletinUserDto? userDto = await _repository.ChangePhoneAsync(id, phone, cancellationToken);
        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> BlockUserAsync(Guid id, CancellationToken cancellationToken)
    {
        BulletinUserDto? userDto;
        userDto = await _repository.GetByIdAsync(id);
        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto = await _repository.SetUserBlockStatusAsync(id, true, cancellationToken);

        return userDto!;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> UnBlockUserAsync(Guid id, CancellationToken cancellationToken)
    {
        BulletinUserDto? userDto;
        userDto = await _repository.GetByIdAsync(id);
        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto = await _repository.SetUserBlockStatusAsync(id, false, cancellationToken);

        return userDto!;
    }
}
