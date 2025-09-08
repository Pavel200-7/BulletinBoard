namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristic
{
    public class BulletinsCharacteristicCreateDto
    {
        public Guid BelletinId { get; set; }

        public Guid CharacteristicNameId { get; set; }

        public Guid CharacteristicValueId { get; set; }
    }
}
