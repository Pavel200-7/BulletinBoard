using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using BulletinBoard.Infrastructure.DataAccess.Repositories;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinMainRepository :
    BaseRepository
    <
    BulletinMain,
    BulletinMainDto,
    BulletinMainCreateDto,
    BulletinMainUpdateDto,
    BulletinContext
    >,
    IBulletinMainRepository
{
    public BulletinMainRepository(IRepository<BulletinMain, BulletinContext> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}