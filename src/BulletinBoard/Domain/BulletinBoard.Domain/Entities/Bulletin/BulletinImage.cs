using BulletinBoard.Domain.Base;


namespace BulletinBoard.Domain.Entities.Bulletin;

public class BulletinImage : EntityBase
{
    public Guid Id { get; set; }

    public Guid BelletinId { get; set; }

    public bool IsMain { get; set; }

    public string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Path { get; set; }
}
