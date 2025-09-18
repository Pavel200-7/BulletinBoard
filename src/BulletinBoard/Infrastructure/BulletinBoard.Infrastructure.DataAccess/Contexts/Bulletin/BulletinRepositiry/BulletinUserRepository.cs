using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Repositories;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepository;

/// <inheritdoc/>
public class BulletinUserRepository :
    BaseRepository
    <
    BulletinUser,
    BulletinUserDto,
    BulletinUserCreateDto,
    BulletinUserUpdateDto,
    BulletinContext
    >,
    IBulletinUserRepository
{
    public BulletinUserRepository(IRepository<BulletinUser, BulletinContext> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}