using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

public class BulletinCharacteristicValueRepository : BulletinBaseRepository, IBulletinsCharacteristicValueRepository
{
    protected readonly DbSet<BulletinCharacteristicValue> _dbSet;

    public BulletinCharacteristicValueRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinCharacteristicValue>();
    }
}
