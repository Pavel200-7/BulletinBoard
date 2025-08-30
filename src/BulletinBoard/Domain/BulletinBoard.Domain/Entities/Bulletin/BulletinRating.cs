using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.Bulletin
{
    public class BulletinRating : EntityBase
    {
        public Guid BulletinId { get; set; }

        public decimal Rating { get; set; }

        public int VievsCount { get; set; }
    }
}
