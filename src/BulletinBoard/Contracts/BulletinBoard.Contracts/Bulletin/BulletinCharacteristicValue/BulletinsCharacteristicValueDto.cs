namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue
{
    public class BulletinsCharacteristicValueDto
    {
        public Guid Id { get; set; }

        public Guid ConnectedNameId { get; set; }

        public string Value { get; set; }
    }
}
