using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы со связями характик с объявлеями.
/// </summary>
public interface IBulletinCharacteristicComparisonService
{
    /// <summary>
    /// Получить связь характики с объявлением по идентификатору.
    /// </summary>
    /// <param name="id">Id связи характики с объявлением.</param>
    /// <returns>Базовый формат данных сопоставления характеристики с объявлением.</returns>
    public Task<BulletinCharacteristicComparisonDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать новое сопоставление характеристики с объявлением.
    /// </summary>
    /// <param name="сharacteristicComparisonDto">Формат данных создания сопоставления характеристики с объявлением.</param>
    /// <returns>Базовый формат данных сопоставления характеристики с объявлением.</returns>
    public Task<BulletinCharacteristicComparisonDto> CreateAsync(BulletinCharacteristicComparisonCreateDto сharacteristicComparisonDto);

    /// <summary>
    /// Обновить существующее сопоставление характеристики с объявлением.
    /// </summary>
    /// <param name="id">Id сопоставления характеристики с объявлением для обновления.</param>
    /// <param name="сharacteristicComparisonDto">Данные для обновления сопоставления характеристики с объявлением.</param>
    /// <returns>Базовый формат данных сопоставления характеристики с объявлением.</returns>
    public Task<BulletinCharacteristicComparisonDto> UpdateAsync(Guid id, BulletinCharacteristicComparisonUpdateDto сharacteristicComparisonDto);

    /// <summary>
    /// Удалить сопоставление характеристики с объявлением по идентификатору.
    /// </summary>
    /// <param name="id">Id сопоставления характеристики с объявлением.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
