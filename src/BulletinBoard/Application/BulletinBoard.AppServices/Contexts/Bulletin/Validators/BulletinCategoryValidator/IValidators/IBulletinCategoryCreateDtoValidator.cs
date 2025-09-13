using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО создания категории по правилам:
/// ParentCategoryId:
///     1. Категория с таким id существует.
///     2. Категория с таким id не является листовой.
/// CategoryName:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 50.
///     3. Может хранить только русские, английские буквы нижнего, верхнего и пробелы.
///     4. Является уникальным.
/// IsLeafy:
///     1. Не пустая строка и не null.
/// </summary>
public interface IBulletinCategoryCreateDtoValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityDto">Формат данных создания категории.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(BulletinCategoryCreateDto entityDto, CancellationToken cancellation = default);
}
