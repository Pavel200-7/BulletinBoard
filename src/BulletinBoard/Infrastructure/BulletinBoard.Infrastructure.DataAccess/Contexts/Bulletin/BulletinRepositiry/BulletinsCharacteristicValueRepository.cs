using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry
{
    public class BulletinsCharacteristicValueRepository : BulletinBaseRepository, IBulletinsCharacteristicValueRepository
    {
        public BulletinsCharacteristicValueRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
