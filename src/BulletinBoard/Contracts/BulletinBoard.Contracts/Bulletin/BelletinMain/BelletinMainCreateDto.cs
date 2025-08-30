using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BelletinMain
{
    public class BelletinMainCreateDto
    {
        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Hidden { get; set; }

        public bool Closed { get; set; }

        public bool Blocked { get; set; }
    }
}
