using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;

/// <summary>
/// Этот класс является ДТО, который нужен только для валидации изменения.
/// Суть в чем: для каждой сущности в этом проекте введен некий стандарт:
/// для 1 сущности предусматривается 4 базовых DTO:
///     1. Базовый ДТО сущности.
///     2. ДТО создания.
///     3. ДТО обновления.
///     4. ДТО фильтрации (максимально полной).
///     5. Все остальные ДТО, которые так или иначе присутствуют в верхних API.
/// Это ДТО является исключением т к оно нужно для особого случая, когда 
/// для валидации нужно поле, которое по хорошему нельзя брать от клиента, и 
/// тогда получить его можно только в сервисе. 
/// Вводить сложную валидацию там я не хочу.
/// 
/// Есть другой вариант исправления этого, но он создает зависимость от БД.
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
