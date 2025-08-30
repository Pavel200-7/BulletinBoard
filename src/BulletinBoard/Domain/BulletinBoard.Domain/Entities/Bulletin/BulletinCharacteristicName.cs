using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.Bulletin
{
    public class BulletinCharacteristicName : EntityBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
