using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Repositories;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinCategoryRepository : 
    BaseRepository
    <
    BulletinCategory, 
    BulletinCategoryDto, 
    BulletinCategoryCreateDto, 
    BulletinCategoryUpdateDto, 
    BulletinContext
    >,
    IBulletinCategoryRepository
{
    public BulletinCategoryRepository(IRepository<BulletinCategory, BulletinContext> repository, IMapper mapper)
        : base(repository, mapper)
    {


    }
}
