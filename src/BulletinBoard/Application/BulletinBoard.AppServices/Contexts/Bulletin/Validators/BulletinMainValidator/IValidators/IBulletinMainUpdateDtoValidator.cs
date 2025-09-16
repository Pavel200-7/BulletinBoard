using FluentValidation.Results;
using BulletinBoard.Contracts.Bulletin.BelletinMain;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО обновления объявления по правилам:
/// Title:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 100.
///     3. Может хранить только русские, английские буквы нижнего, верхнего и пробелы.
///     4. Является уникальным.
/// Description:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 1000.
///     3. Может хранить только русские, английские буквы нижнего, верхнего и пробелы.
/// Price:
///     1. Соответствует типу данных decimal.
///     2. Не является отрицательным.
/// </summary>
public interface IBulletinMainUpdateDtoValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityDto">Формат данных обновления объявления.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(BulletinMainUpdateDto entityDto, CancellationToken cancellation = default);
}
