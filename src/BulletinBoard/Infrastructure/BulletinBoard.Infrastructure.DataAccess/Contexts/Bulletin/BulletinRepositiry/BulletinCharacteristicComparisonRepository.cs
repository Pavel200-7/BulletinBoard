using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

public class BulletinCharacteristicComparisonRepository : BulletinBaseRepository, IBulletinCharacteristicComparisonRepository
{
    protected readonly DbSet<BulletinCharacteristicComparison> _dbSet;

    public BulletinCharacteristicComparisonRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinCharacteristicComparison>();
    }
}
