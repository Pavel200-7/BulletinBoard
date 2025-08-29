using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin
{
    public class BulletinDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Hidden {  get; set; }

        public bool Closed { get; set; }

    }
}
