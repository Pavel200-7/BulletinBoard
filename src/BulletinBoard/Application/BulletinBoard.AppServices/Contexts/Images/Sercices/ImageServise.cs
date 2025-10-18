using BulletinBoard.AppServices.Contexts.Images.Repository;
using BulletinBoard.AppServices.Contexts.Images.Sercices.IServices;
using BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator.IValidators;
using BulletinBoard.AppServices.Contexts.User.Validators.UserValidator.IValidators;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.Images.Image.ReadDto;


namespace BulletinBoard.AppServices.Contexts.Images.Sercices;

/// <inheritdoc/>
public class ImageServise : IImageServise
{
    private readonly IImageRepository _imageСacheRepository;
    private readonly IImageValidatorFacade _validator;

    /// <inheritdoc/>
    public ImageServise
        (
        IImageRepository imageСacheRepository,
        IImageValidatorFacade validator
        )
    {
        _imageСacheRepository = imageСacheRepository;
        _validator = validator;
    }
    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(ImageCreateDto createDto, CancellationToken cancellationToken)
    {
        await _validator.ValidateThrowValidationExeptionAsync(createDto);
        Guid imageId = await _imageСacheRepository.UploadAsync(createDto, cancellationToken);
        return imageId;
    }

    /// <inheritdoc/>
    public async Task<ImageReadDto> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        var outputDto = await _imageСacheRepository.DownloadAsync(id, cancellationToken);
        if (outputDto is null) { throw new NotFoundException(GetNotFoundMessage(id)); }
        return outputDto;
    }

    /// <inheritdoc/>
    public async Task<ImageInfoReadDto> GetMetaDataAsync(Guid id, CancellationToken cancellationToken)
    {
        var outputDto = await _imageСacheRepository.GetMetaDataAsync(id, cancellationToken);
        if (outputDto is null) { throw new NotFoundException(GetNotFoundMessage(id)); }
        return outputDto;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        bool isOnDeleting = await _imageСacheRepository.DeleteAsync(id, cancellationToken);
        if (!isOnDeleting) { throw new NotFoundException(GetNotFoundMessage(id)); }
        return isOnDeleting;
    }

    private string GetNotFoundMessage(Guid id)
    {
        return $"The image with id {id} is not found.";

    }


}
