using BulletinBoard.AppServices.Contexts.Images.Repository;
using BulletinBoard.AppServices.Contexts.Images.Sercices.IServices;
using BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator.IValidators;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.Images.Image.ReadDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Images.Sercices;

/// <inheritdoc/>
public class ImageСacheServise : IImageСacheServise
{
    private readonly IImageСacheRepository _imageСacheRepository;
    private readonly IImageValidatorFacade _validator;

    /// <inheritdoc/>
    public ImageСacheServise(IImageСacheRepository imageСacheRepository)
    {
        _imageСacheRepository = imageСacheRepository;
    }
    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(ImageCreateDto createDto, CancellationToken cancellationToken)
    {
        await _validator.ValidateThrowValidationExeptionAsync(createDto);
        Guid imageId = await _imageСacheRepository.UploadAsync(createDto, cancellationToken);
        return imageId;
    }

    /// <inheritdoc/>
    public async Task<ImageReadDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        var outputDto = await _imageСacheRepository.DownloadAsync(id, cancellationToken);
        if (outputDto is null) { throw new NotFoundException($"The image with id {id} is not found."); }
        return outputDto;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        bool isOnDeleting = await _imageСacheRepository.DeleteAsync(id, cancellationToken);
        if (!isOnDeleting) { throw new NotFoundException($"The image with id {id} is not found."); }
        return isOnDeleting;
    }
}
