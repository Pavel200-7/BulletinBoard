using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

public class BulletinsCharacteristicValueRepository : BulletinBaseRepository, IBulletinsCharacteristicValueRepository
{
    public BulletinsCharacteristicValueRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
