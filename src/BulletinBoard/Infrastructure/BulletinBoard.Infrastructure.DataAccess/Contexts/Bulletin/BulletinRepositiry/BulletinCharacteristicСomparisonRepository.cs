using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

public class BulletinCharacteristicСomparisonRepository : BulletinBaseRepository, IBulletinsCharacteristicRepository
{
    protected readonly DbSet<BulletinCharacteristicСomparison> _dbSet;

    public BulletinCharacteristicСomparisonRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinCharacteristicСomparison>();
    }
}
