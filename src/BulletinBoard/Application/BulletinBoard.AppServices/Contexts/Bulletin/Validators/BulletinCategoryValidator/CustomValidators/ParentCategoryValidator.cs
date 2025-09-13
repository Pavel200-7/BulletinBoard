using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using FluentValidation;
using FluentValidation.Validators;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators;

/// <summary>
/// Класс берущий на себя задачу валидации данных по 
/// ограничениям, проверка которых требует обращения к БД.
/// </summary>
public class ParentCategoryValidator<T> : AsyncPropertyValidator<T, Guid?>
{
    /// <summary>
    /// Название валидатора
    /// </summary>
    public override string Name => "ParentCategoryValidator";

    private readonly IBulletinCategoryRepository _categoryRepository;

    /// <inheritdoc/>
    public ParentCategoryValidator(IBulletinCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Валидация 
    /// </summary>
    public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid? parentId, CancellationToken cancellation)
    {
        // Если parentId null - validation passes
        if (!parentId.HasValue)
            return true;

        // Получаем категорию одним запросом
        var category = await _categoryRepository.GetByIdAsync(parentId.Value);

        // Проверяем оба условия
        if (category == null)
        {
            context.MessageFormatter.AppendArgument("Error", "Parent category with this id does not exist.");
            return false;
        }

        if (category.IsLeafy)
        {
            context.MessageFormatter.AppendArgument("Error", "Parent category with this id is Leafy and can not have children.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Базовое сообщение об ошибке 
    /// </summary>
    protected override string GetDefaultMessageTemplate(string errorCode)
        => "{Error}";
}