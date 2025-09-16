using FluentValidation.Results;
using BulletinBoard.Contracts.Bulletin.BelletinMain;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО создания объявления по правилам:
/// BulletinUserId:
///     1. Пользователь с таким id существует.
///     2. Пользователь с таким id не заблокирован.
/// Title:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 100.
///     3. Может хранить только русские, английские буквы нижнего, верхнего и пробелы.
///     4. Является уникальным.
/// Description:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 1000.
///     3. Может хранить только русские, английские буквы нижнего, верхнего и пробелы.
/// ParentCategoryId:
///     1. Категория с таким id существует.
///     2. Категория с таким id является листовой.
/// Price:
///     1. Соответствует типу данных decimal.
///     2. Не является отрицательным.
/// </summary>
public interface IBulletinMainCreateDtoValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityDto">Формат данных создания объявления.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(BulletinMainCreateDto entityDto, CancellationToken cancellation = default);
}
