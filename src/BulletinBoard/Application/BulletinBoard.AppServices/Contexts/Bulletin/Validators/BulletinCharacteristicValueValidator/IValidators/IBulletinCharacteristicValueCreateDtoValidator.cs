using FluentValidation.Results;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО создания значения характеристики объявления по правилам:
/// CharacteristicId:
///     1. Характеристика с таким id существует.
/// Value:
///     ///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 35.
///     3. Может хранить только русские, английские буквы нижнего, верхнего регистра и пробелы и цифры.
///     4. Является уникальным для выбранной характеристики.
/// </summary>
public interface IBulletinCharacteristicValueCreateDtoValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityDto">Формат данных создания объявления.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(BulletinCharacteristicValueCreateDto entityDto, CancellationToken cancellation = default);
}
