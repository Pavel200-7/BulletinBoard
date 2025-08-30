using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinsCharacteristics
{
    public class BulletinsCharacteristicCreateDto
    {
        public Guid BelletinId { get; set; }

        public Guid CharacteristicNameId { get; set; }

        public Guid CharacteristicValueId { get; set; }
    }
}
