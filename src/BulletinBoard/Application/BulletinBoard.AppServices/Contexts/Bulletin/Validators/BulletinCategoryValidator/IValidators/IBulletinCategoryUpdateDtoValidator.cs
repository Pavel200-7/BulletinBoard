using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО одновления категории по правилам:
/// ParentCategoryId:
///     1. Категория с таким id существует.
///     2. Категория с таким id не является листовой.
/// CategoryName:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 50.
///     3. Может хранить только русские, английские буквы нижнего, верхнего регистра и пробелы.
///     4. Является уникальным.
/// </summary>
public interface IBulletinCategoryUpdateDtoValidator 
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityDto">Формат данных изменения категории.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(BulletinCategoryUpdateDto entityDto, CancellationToken cancellation = default);
}
