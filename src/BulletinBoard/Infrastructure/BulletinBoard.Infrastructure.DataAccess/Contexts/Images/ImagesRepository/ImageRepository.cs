using AutoMapper;
using AutoMapper.QueryableExtensions;
using BulletinBoard.AppServices.Contexts.Images.Repository;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.Images.Image.ReadDto;
using BulletinBoard.Domain.Entities.Images;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Images.ImagesRepository;

/// <inheritdoc/>
public class ImageRepository : IImageRepository
{
    private readonly IRepository<Image, ImagesContext> _repository;
    private IMapper _autoMapper;

    /// <inheritdoc/>
    public ImageRepository
        (
        IRepository<Image, ImagesContext> repository,
        IMapper autoMapper
        )
    {
        _repository = repository;
        _autoMapper = autoMapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(ImageCreateDto createDto, CancellationToken cancellationToken)
    {
        var image = _autoMapper.Map<Image>(createDto);
        await _repository.AddAsync(image, cancellationToken);
        return image.Id;
    }

    /// <inheritdoc/>
    public async Task<ImageInfoReadDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetAll().Where(s => s.Id == id)
            .ProjectTo<ImageInfoReadDto>(_autoMapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ImageReadDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetAll().Where(s => s.Id == id)
            .ProjectTo<ImageReadDto>(_autoMapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return false;

        await _repository.DeleteAsync(id, cancellationToken);
        return true;
    }
}
