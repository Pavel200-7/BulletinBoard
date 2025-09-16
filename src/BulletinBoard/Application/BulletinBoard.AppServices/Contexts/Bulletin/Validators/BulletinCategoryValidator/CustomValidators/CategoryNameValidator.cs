using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation;
using FluentValidation.Validators;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators;

/// <summary>
/// Класс берущий на себя задачу валидации данных по 
/// ограничениям, проверка которых требует обращения к БД.
/// Если конкретно, то он проверяет, существует ли категория с таким названием.
/// </summary>
public class CategoryNameValidator<T> : AsyncPropertyValidator<T, string> 
{
    /// <summary>
    /// Название валидатора
    /// </summary>
    public override string Name => "CategoryNameValidator";

    private readonly IBulletinCategoryRepository _categoryRepository;
    private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public CategoryNameValidator
        (
        IBulletinCategoryRepository categoryRepository, 
        IBulletinCategorySpecificationBuilder specificationBuilder
        )
    {
        _categoryRepository = categoryRepository;
        _specificationBuilder = specificationBuilder;
    }

    /// <summary>
    /// Валидация 
    /// </summary>
    public override async Task<bool> IsValidAsync(ValidationContext<T> context, string categoryName, CancellationToken cancellation)
    {
        ExtendedSpecification<BulletinCategory> specification = _specificationBuilder
            .WhereCategoryName(categoryName)
            .Build();

        var categories = await _categoryRepository.FindAsync(specification);

        if (categories.Any())
        {
            context.MessageFormatter.AppendArgument("Error", "A category with this name is already exist.");
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
