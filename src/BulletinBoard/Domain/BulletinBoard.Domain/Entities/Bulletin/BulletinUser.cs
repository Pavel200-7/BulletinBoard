using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.Bulletin;

public class BulletinUser : EntityBase
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public bool Blocked { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string? FormattedAddress { get; set; } 
}
