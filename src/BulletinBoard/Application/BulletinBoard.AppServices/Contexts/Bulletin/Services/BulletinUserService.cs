using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser.FilterDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser.UpdateDto;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;



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
    public async Task<BulletinUserDto> ChangeNameAsync(Guid id, string name, CancellationToken cancellationToken)
    {
        BulletinUserDto? userDto = await _repository.ChangeNameAsync(id, name, cancellationToken);
        if (userDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }
        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangeAdressAsync(Guid id, BulletinUserUpdateLocationDto userLocationDto, CancellationToken cancellationToken)
    {
        BulletinUserDto? userDto = await _repository.ChangeAdressAsync(id, userLocationDto, cancellationToken);
        if (userDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }
        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> ChangePhoneAsync(Guid id, string phone, CancellationToken cancellationToken)
    {
        BulletinUserDto? userDto = await _repository.ChangePhoneAsync(id, phone, cancellationToken);
        if (userDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }
        return userDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> BlockUserAsync(Guid id, CancellationToken cancellationToken)
    {
        BulletinUserDto? userDto;
        userDto = await _repository.GetByIdAsync(id);
        if (userDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }
        userDto = await _repository.SetUserBlockStatusAsync(id, true, cancellationToken);
        return userDto!;
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> UnBlockUserAsync(Guid id, CancellationToken cancellationToken)
    {
        BulletinUserDto? userDto;
        userDto = await _repository.GetByIdAsync(id);
        if (userDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }
        userDto = await _repository.SetUserBlockStatusAsync(id, false, cancellationToken);
        return userDto!;
    }
}
