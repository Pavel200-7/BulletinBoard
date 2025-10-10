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
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Images.ImagesRepository;

/// <inheritdoc/>
public class ImageСacheRepository : IImageСacheRepository
{
    private readonly IGridFSBucket _gridFS;
    private readonly IMongoCollection<ImageСache> _metadataCollection;

    public ImageСacheRepository(IMongoDatabase database)
    {
        _gridFS = new GridFSBucket(database, new GridFSBucketOptions
        {
            BucketName = "imagesCache",
            ChunkSizeBytes = 255 * 1024 // 255KB chunks
        });

        _metadataCollection = database.GetCollection<ImageСache>("ImagesCache");
    }


    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(ImageCreateDto createDto, CancellationToken cancellationToken)
    {
        var imageId = createDto.Id != Guid.Empty ? createDto.Id : Guid.NewGuid();

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
        var fileId = await _gridFS.UploadFromStreamAsync(
            createDto.Name, stream, options, cancellationToken);

        var metadata = new ImageСache
        {
            Id = imageId,
            Name = createDto.Name,
            ContentType = createDto.ContentType,
            Length = createDto.Length
        };

        await _metadataCollection.InsertOneAsync(metadata, cancellationToken: cancellationToken);
        return imageId;
    }
        

    /// <inheritdoc/>
    public async Task<ImageReadDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {

    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {

    }

}
