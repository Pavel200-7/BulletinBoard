using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinImage.ForValidating;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinImageService : BaseCRUDService
    <
    BulletinImageDto,
    BulletinImageCreateDto,
    BulletinImageUpdateDto,
   BulletinImageUpdateDtoForValidating,
    IBulletinImageRepository,
    IBulletinImageDtoValidatorFacade
    >, IBulletinImageService
{
    /// <inheritdoc/>
    protected override string EntityName { get; } = "image";

    public BulletinImageService
        (
        IBulletinImageRepository repository, 
        IBulletinImageDtoValidatorFacade validator, 
        IMapper autoMapper
        ) : base(repository, validator, autoMapper)
    {
    }

    /// <inheritdoc/>
    protected override Task<BulletinImageUpdateDtoForValidating> GetUpdateValidationDto(Guid id, BulletinImageUpdateDto updateDto)
    {
        var validatinoDto = _autoMapper.Map<BulletinImageUpdateDtoForValidating>(updateDto);
        return Task.FromResult(validatinoDto);
    }
}
