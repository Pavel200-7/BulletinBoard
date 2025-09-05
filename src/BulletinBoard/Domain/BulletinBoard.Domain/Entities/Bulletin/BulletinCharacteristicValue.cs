using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.Bulletin
{
    public class BulletinCharacteristicValue : EntityBase
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ConnectedNameId { get; set; }

        public string Value { get; set; }
    }
}
