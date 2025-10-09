using BulletinBoard.AppServices.Contexts.Images.Repository;
using BulletinBoard.AppServices.Contexts.Images.Sercices.IServices;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.Images.Image.ReadDto;


namespace BulletinBoard.AppServices.Contexts.Images.Sercices;

/// <inheritdoc/>
public class ImageService : IImageServise
{
    private readonly IImageRepository _imageRepository;

    /// <inheritdoc/>
    public ImageService(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(ImageCreateDto createDto, CancellationToken cancellationToken)
    {
        return await _imageRepository.UploadAsync(createDto, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ImageInfoReadDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var outputDto = await _imageRepository.GetInfoByIdAsync(id, cancellationToken);
        if (outputDto is null) { throw new NotFoundException($"The image with id {id} is not found."); }
        return outputDto;
    }

    /// <inheritdoc/>
    public async Task<ImageReadDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        var outputDto = await _imageRepository.DownloadAsync(id, cancellationToken);
        if (outputDto is null) { throw new NotFoundException($"The image with id {id} is not found."); }
        return outputDto;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    { 
        bool isOnDeleting = await _imageRepository.DeleteAsync(id, cancellationToken);
        if (!isOnDeleting) { throw new NotFoundException($"The image with id {id} is not found."); }
        return isOnDeleting;
    }
}