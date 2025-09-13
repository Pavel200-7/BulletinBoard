using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

public class BulletinsCharacteristicName : BulletinBaseRepository, IBulletinsCharacteristicNameRepository
{
    public BulletinsCharacteristicName(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
