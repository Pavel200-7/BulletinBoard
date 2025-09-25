using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.FilterDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с возможным значение характеристики объявления.
/// </summary>
public interface IBulletinCharacteristicValueService : IBaseCRUDService
    <
    BulletinCharacteristicValueDto,
    BulletinCharacteristicValueCreateDto,
    BulletinCharacteristicValueUpdateDto
    >
{
    /// <summary>
    /// Получить список возможных значений характеристики по фильтру.
    /// </summary>
    /// <param name="сharacteristicValueFilterDto">Формат данных для фильтрации значений характеристики.</param>
    /// <returns>Коллекция базового формата данных значений характеристики.</returns>
    public Task<IReadOnlyCollection<BulletinCharacteristicValueDto>> GetAsync(BulletinCharacteristicValueFilterDto сharacteristicValueFilterDto);

    /// <summary>
    /// Получить список значений конкретной характеристики по ее id.
    /// </summary>
    /// <param name="characteristicId">id характеристики</param>
    /// <returns>Коллекция базового формата данных значений характеристики.</returns>
    public Task<IReadOnlyCollection<BulletinCharacteristicValueDto>> GetByCharacteristicAsync(Guid characteristicId);
}
