using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using BulletinBoard.Infrastructure.DataAccess.Repositories;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinRatingRepository :
    BaseRepository
    <
    BulletinRating,
    BulletinRatingDto,
    BulletinRatingCreateDto,
    BulletinRatingUpdateDto,
    BulletinContext
    >,
    IBulletinRatingRepository
{
    public BulletinRatingRepository(IRepository<BulletinRating, BulletinContext> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}