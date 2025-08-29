using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinsCategories
{
    public class BulletinCategoryDto
    {
        public Guid Id { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
