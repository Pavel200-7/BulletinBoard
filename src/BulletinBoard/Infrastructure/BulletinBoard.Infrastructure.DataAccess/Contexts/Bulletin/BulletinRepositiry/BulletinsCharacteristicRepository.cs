using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

public class BulletinsCharacteristicRepository : BulletinBaseRepository, IBulletinsCharacteristicRepository
{
    public BulletinsCharacteristicRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
