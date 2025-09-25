using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.FilterDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с характеристиками объявлений.
/// </summary>
public interface IBulletinCharacteristicService : IBaseCRUDService
    <
    BulletinCharacteristicDto,
    BulletinCharacteristicCreateDto,
    BulletinCharacteristicUpdateDto
    >
{

    /// <summary>
    /// Получить список характеристики по фильтру.
    /// </summary>
    /// <param name="сharacteristicFilterDto">Формат данных для фильтрации хакактеристик.</param>
    /// <returns>Коллекция базовый формат данных характеристики.</returns>
    public Task<IReadOnlyCollection<BulletinCharacteristicDto>> GetAsync(BulletinCharacteristicFilterDto сharacteristicFilterDto);


    /// <summary>
    /// Получить список характеристик по id категории, к которой они относятся.
    /// </summary>
    /// <param name="categoryId">id категории.</param>
    /// <returns>Коллекция базовый формат данных характеристики.</returns>
    public Task<IReadOnlyCollection<BulletinCharacteristicDto>> GetByCategoryFilter(Guid categoryId);
}
