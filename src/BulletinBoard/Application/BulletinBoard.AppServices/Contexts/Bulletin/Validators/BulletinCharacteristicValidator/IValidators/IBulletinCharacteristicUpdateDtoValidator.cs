using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО обновления характеристики по правилам:
/// Name:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 35.
///     3. Может хранить только русские, английские буквы нижнего, верхнего регистра и пробелы и цифры.
///     4. Является уникальным для выбранной категории.
/// <summary>
public interface IBulletinCharacteristicUpdateDtoValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityDto">Формат данных изменения характеристики.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(BulletinCharacteristicUpdateDtoForValidating entityDto, CancellationToken cancellation = default);
}
