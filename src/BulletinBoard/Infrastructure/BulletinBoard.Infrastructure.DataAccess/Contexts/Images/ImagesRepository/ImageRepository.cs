using AutoMapper;
using BulletinBoard.AppServices.Contexts.Images.Repository;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.Images.Image.ReadDto;
using BulletinBoard.Domain.Entities.Images;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Images.ImagesRepository;

/// <inheritdoc/>
public class ImageRepository : IImageRepository
{
    private readonly IGridFSBucket _gridFS;
    private readonly IMongoCollection<ImageMetadata> _metadataCollection;
    private readonly IMapper _automapper;

    public ImageRepository
        (
        IMongoDatabase database,
        IMapper automapper
        )
    {
        _gridFS = new GridFSBucket(database, new GridFSBucketOptions
        {
            BucketName = "images",
            ChunkSizeBytes = 255 * 1024 // 255KB chunks
        });

        _metadataCollection = database.GetCollection<ImageMetadata>("Images");

        _automapper = automapper;
    }


    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(ImageCreateDto createDto, CancellationToken cancellationToken)
    {
        Guid imageId = Guid.NewGuid();

        var options = new GridFSUploadOptions
        {
            Metadata = new BsonDocument
            {
                { "imageId", imageId.ToString() },
                { "contentType", createDto.ContentType },
                { "name", createDto.Name },
                { "uploadedAt", DateTime.UtcNow }
            }
        };

        using var stream = new MemoryStream(createDto.Content);
        var fileId = await _gridFS.UploadFromStreamAsync(createDto.Name, stream, options, cancellationToken);

        ImageMetadata metadata = _automapper.Map<ImageMetadata>(createDto);
        metadata.Id = imageId;
        await _metadataCollection.InsertOneAsync(metadata, cancellationToken: cancellationToken);

        return imageId;
    }


    /// <inheritdoc/>
    public async Task<ImageReadDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        var metadata = await _metadataCollection
            .Find(m => m.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (metadata == null) return null;

        var filter = Builders<GridFSFileInfo>.Filter.Eq("metadata.imageId", id.ToString());
        var fileInfo = await _gridFS.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (fileInfo == null) return null;

        using var stream = new MemoryStream();
        await _gridFS.DownloadToStreamAsync(fileInfo.Id, stream);

        return new ImageReadDto
        {
            Name = metadata.Name,
            Content = stream.ToArray(),
            ContentType = metadata.ContentType
        };
    }

    public async Task<ImageInfoReadDto?> GetMetaDataAsync(Guid id, CancellationToken cancellationToken)
    {
        ImageMetadata? metadata = await _metadataCollection
            .Find(m => m.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (metadata is null) return null;

        var metadataDto = _automapper.Map<ImageInfoReadDto>(metadata);
        return metadataDto;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<GridFSFileInfo>.Filter.Eq("metadata.imageId", id.ToString());
        var fileInfo = await _gridFS.Find(filter).FirstOrDefaultAsync(cancellationToken);

        if (fileInfo == null) return false;

        await _gridFS.DeleteAsync(fileInfo.Id, cancellationToken);
        var deleteResult = await _metadataCollection.DeleteOneAsync(
            m => m.Id == id, cancellationToken);

        return deleteResult.DeletedCount > 0;
    }
}
