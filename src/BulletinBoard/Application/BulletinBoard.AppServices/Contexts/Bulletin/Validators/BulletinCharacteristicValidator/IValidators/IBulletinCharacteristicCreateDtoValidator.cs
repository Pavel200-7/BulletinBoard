using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО создания характеристики по правилам:
/// Name:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 35.
///     3. Может хранить только русские, английские буквы нижнего, верхнего регистра и пробелы и цифры.
///     4. Является уникальным для выбранной категории.
/// CategoryId:
///     1. Категория с таким id существует.
///     2. Категория с таким id является листовой.
///     3. Не null.
/// <summary>
public interface IBulletinCharacteristicCreateDtoValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityDto">Формат данных создания характеристики.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(BulletinCharacteristicCreateDto entityDto, CancellationToken cancellation = default);
}
