using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinCategoryValue
{
    public class BulletinsCharacteristicValueDto
    {
        public Guid Id { get; set; }

        public Guid ConnectedNameId { get; set; }

        public string Value { get; set; }
    }
}
