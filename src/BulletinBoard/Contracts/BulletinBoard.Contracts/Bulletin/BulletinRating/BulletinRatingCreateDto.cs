using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinRating
{
    public class BulletinRatingCreateDto
    {
        public Guid BulletinId { get; set; }

        public decimal Rating { get; set; }

        public int VievsCount { get; set; }
    }
}
