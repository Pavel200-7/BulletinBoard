using AutoMapper;
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

    private readonly IMapper _mapper;
    //private readonly IBulletinUserSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinUserService
        (
            IBulletinUserRepository bulletinCategoryRepository,
            IBulletinUserSpecificationBuilder specificationBuilder,
            IMapper mapper
        )
    {
        _userRepository = bulletinCategoryRepository;
        _specificationBuilder = specificationBuilder;
        _mapper = mapper;
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

        return outputUserDto!;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinUserDto>> GetAsync(BulletinUserFilterDto userDto)
    {
        if (userDto.IsUsedFullName)
        {
            _specificationBuilder.WhereFullName(userDto.FullName);
        } 
        else if (userDto.IsUsedFullNameContains)
        {
            _specificationBuilder.WhereFullNameContains(userDto.FullName);
        }

        if (userDto.IsUsedBlocked)
        {
            _specificationBuilder.WhereIsBlocked(userDto.Blocked);
        }

        if (userDto.IsUsedCoordinates)
        {
            if (userDto.IsUsedCoordinatesEquals)
            {
                _specificationBuilder.WhereCoordinates(userDto.Latitude, userDto.Longitude);
            }
            else if (userDto.IsUsedCoordinatesCloser)
            {
                _specificationBuilder.WhereCoordinatesCloser(userDto.Latitude, userDto.Longitude, userDto.Distance);
            }
            else if (userDto.IsUsedCoordinatesFarther)
            {
                _specificationBuilder.WhereCoordinatesFarther(userDto.Latitude, userDto.Longitude, userDto.Distance);
            }
        }

        if (userDto.IsUsedFormattedAddress)
        {
            _specificationBuilder.WhereFormattedAddress(userDto.FormattedAddress);
        }

        if (userDto.IsUsedPhone)
        {
            _specificationBuilder.WherePhone(userDto.Phone);
        }

        if (userDto.IsUsedCoordinates)
        {
            switch (true)
            {
                case true when userDto.IsUsedCoordinatesEquals == true:
                    _specificationBuilder.WhereCoordinates(userDto.Latitude, userDto.Longitude);
                    break;
                case true when userDto.IsUsedCoordinatesCloser == true:
                    _specificationBuilder.WhereCoordinatesCloser(userDto.Latitude, userDto.Longitude, userDto.Distance);
                    break;
                case true when userDto.IsUsedCoordinatesFarther == true:
                    _specificationBuilder.WhereCoordinatesFarther(userDto.Latitude, userDto.Longitude, userDto.Distance);
                    break;
            }
        }

        ExtendedSpecification<BulletinUser> specification = _specificationBuilder
            .Build();

        IReadOnlyCollection<BulletinUserDto> userDtoCollectrion = await _userRepository.FindAsync(specification);

        return userDtoCollectrion;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> CreateAsync(BulletinUserCreateDto userDto)
    {
        BulletinUserDto outputUserDto = await _userRepository.CreateAsync(userDto);
        //await _userRepository.SaveChangesAsync();

        return outputUserDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangeNameAsync(Guid id, string name)
    {
        BulletinUserDto? userDto = await _userRepository.GetByIdAsync(id);

        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto.FullName = name;
        BulletinUserUpdateDto userUpdateDto = _mapper.Map<BulletinUserUpdateDto>(userDto);
        await _userRepository.UpdateAsync(id, userUpdateDto);
        //await _userRepository.SaveChangesAsync();

        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangeAdressAsync(Guid id, BulletinUserUpdateLocationDto userLocationDto)
    {
        BulletinUserDto? userDto = await _userRepository.GetByIdAsync(id);

        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto = _mapper.Map<BulletinUserDto>(userLocationDto);
        BulletinUserUpdateDto userUpdateDto = _mapper.Map<BulletinUserUpdateDto>(userDto);
        await _userRepository.UpdateAsync(id, userUpdateDto);
        //await _userRepository.SaveChangesAsync();

        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangePhoneAsync(Guid id, string phone)
    {
        BulletinUserDto? userDto = await _userRepository.GetByIdAsync(id);

        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto.Phone = phone;
        BulletinUserUpdateDto userUpdateDto = _mapper.Map<BulletinUserUpdateDto>(userDto);
        await _userRepository.UpdateAsync(id, userUpdateDto);
        //await _userRepository.SaveChangesAsync();

        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> BlockUserAsync(Guid id)
    {
        BulletinUserDto? userDto = await _userRepository.GetByIdAsync(id);

        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto.Blocked = true;
        BulletinUserUpdateDto userUpdateDto = _mapper.Map<BulletinUserUpdateDto>(userDto);
        await _userRepository.UpdateAsync(id, userUpdateDto);
        //await _userRepository.SaveChangesAsync();

        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> UnBlockUserAsync(Guid id)
    {
        BulletinUserDto? userDto = await _userRepository.GetByIdAsync(id);

        if (userDto is null)
        {
            string errorMessage = $"The user with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        userDto.Blocked = false;
        BulletinUserUpdateDto userUpdateDto = _mapper.Map<BulletinUserUpdateDto>(userDto);
        await _userRepository.UpdateAsync(id, userUpdateDto);
        //await _userRepository.SaveChangesAsync();

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

        //await _userRepository.SaveChangesAsync();

        return isOnDeleting;
    }
}
