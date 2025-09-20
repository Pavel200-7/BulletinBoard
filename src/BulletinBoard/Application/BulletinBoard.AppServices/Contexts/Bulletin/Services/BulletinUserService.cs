using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinUserService : IBulletinUserService
{
    private readonly IBulletinUserRepository _userRepository;
    private readonly IBulletinUserSpecificationBuilder _specificationBuilder;


    /// <inheritdoc/>
    public BulletinUserService
        (
            IBulletinUserRepository bulletinCategoryRepository,
            IBulletinUserSpecificationBuilder specificationBuilder
        )
    {
        _userRepository = bulletinCategoryRepository;
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> GetByIdAsync(Guid id)
    {
        BulletinUserDto? outputUserDto = await _userRepository.GetByIdAsync(id);

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

        IReadOnlyCollection<BulletinUserDto> userDtoCollection = await _userRepository.FindAsync(specification);

        return userDtoCollection;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> CreateAsync(BulletinUserCreateDto userDto)
    {
        BulletinUserDto outputUserDto = await _userRepository.CreateAsync(userDto);
        return outputUserDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangeNameAsync(Guid id, string name)
    {
        BulletinUserDto? userDto = await _userRepository.ChangeNameAsync(id, name);

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
        bool isOnDeleting = await _userRepository.DeleteAsync(id);

        if (!isOnDeleting)
        {
            string errorMessage = $"The note with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return isOnDeleting;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangeAdressAsync(Guid id, BulletinUserUpdateLocationDto userLocationDto)
    {
        BulletinUserDto? userDto = await _userRepository.ChangeAdressAsync(id, userLocationDto);

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
        BulletinUserDto? userDto = await _userRepository.ChangePhoneAsync(id, phone);

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
        userDto = await _userRepository.GetByIdAsync(id);

        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto = await _userRepository.SetUserBlockStatusAsync(id, true);

        return userDto!;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> UnBlockUserAsync(Guid id)
    {
        BulletinUserDto? userDto;
        userDto = await _userRepository.GetByIdAsync(id);

        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto = await _userRepository.SetUserBlockStatusAsync(id, false);


        return userDto!;
    }
}
