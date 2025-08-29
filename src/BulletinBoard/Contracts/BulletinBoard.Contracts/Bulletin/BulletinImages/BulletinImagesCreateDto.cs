using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinImages
{
    public class BulletinImagesCreateDto
    {
        public Guid BelletinId { get; set; }

        public bool IsMain { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Path { get; set; }
    }
}
