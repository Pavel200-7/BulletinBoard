using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinRatingService : BaseCRUDService
    <
    BulletinRatingDto,
    BulletinRatingCreateDto,
    BulletinRatingUpdateDto,
    BulletinRatingUpdateDtoForValidating,
    IBulletinRatingRepository,
    IBulletinRatingDtoValidatorFacade
    >, IBulletinRatingService
{
    /// <inheritdoc/>
    protected override string EntityName { get; } = "rating";

    private readonly IBulletinRatingSpecificationBuilder _specificationBuilder;


    /// <inheritdoc/>
    public BulletinRatingService
        (
        IBulletinRatingRepository repository,
        IBulletinRatingDtoValidatorFacade validator,
        IMapper autoMapper,
        IBulletinRatingSpecificationBuilder specificationBuilder
        ) : base(repository, validator, autoMapper)
    {
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    protected override Task<BulletinRatingUpdateDtoForValidating> GetUpdateValidationDto(Guid id, BulletinRatingUpdateDto updateDto)
    {
        var validatinoDto = _autoMapper.Map<BulletinRatingUpdateDtoForValidating>(updateDto);
        return Task.FromResult(validatinoDto);
    }


}
