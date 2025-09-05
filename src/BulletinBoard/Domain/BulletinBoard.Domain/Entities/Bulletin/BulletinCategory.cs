using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.Bulletin
{
    public class BulletinCategory : EntityBase
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool IsLeafy { get; set; }
    }
}
