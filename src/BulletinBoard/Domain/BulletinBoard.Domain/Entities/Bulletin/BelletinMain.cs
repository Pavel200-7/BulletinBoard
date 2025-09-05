using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BulletinBoard.Domain.Entities.Bulletin
{
    public class BelletinMain : EntityBase 
    {
        [Key]
        public Guid Id { get; set; }

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
