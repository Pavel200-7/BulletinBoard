using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.Bulletin
{
    public class BulletinCategoryValue : EntityBase
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}
