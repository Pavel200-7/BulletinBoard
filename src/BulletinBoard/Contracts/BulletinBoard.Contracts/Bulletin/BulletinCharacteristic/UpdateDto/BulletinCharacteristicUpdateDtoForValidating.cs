namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.UpdateDto;

/// <summary>
/// ДТО для валидации изменения.
/// </summary>
public class BulletinCharacteristicUpdateDtoForValidating
{
    /// <summary>
    /// Наименование характеристики
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Id Категории объявления
    /// </summary>
    public Guid CategoryId { get; set; }

}
