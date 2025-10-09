using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.UpdateDto;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Repositories;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepository;

/// <inheritdoc/>
public class BulletinImageRepository :
    BaseRepository
    <
    BulletinImage,
    BulletinImageDto,
    BulletinImageCreateDto,
    BulletinImageUpdateDto,
    BulletinContext
    >,
    IBulletinImageRepository
{
    public BulletinImageRepository(IRepository<BulletinImage, BulletinContext> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}
