using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.Bulletin
{
    public class BulletinImages : EntityBase
    {
        public Guid Id { get; set; }

        public Guid BelletinId { get; set; }

        public bool IsMain { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Path { get; set; }
    }
}
