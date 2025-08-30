using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinCategory
{
    public class BulletinCategoryDto
    {
        public Guid Id { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool IsLeafy { get; set; }

    }
}
