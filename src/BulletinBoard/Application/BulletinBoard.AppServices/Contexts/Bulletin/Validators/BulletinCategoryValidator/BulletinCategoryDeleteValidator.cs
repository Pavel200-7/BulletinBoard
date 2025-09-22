using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;

using FluentValidation;



namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;

/// <inheritdoc/>
public class BulletinCategoryDeleteValidator : AbstractValidator<Guid>, IBulletinCategoryDeleteValidator
{
    private readonly IBulletinCategoryRepository _categoryRepository;
    private readonly IBulletinCategorySpecificationBuilder _categorySpecificationBuilder;

    /// <inheritdoc/>
    public BulletinCategoryDeleteValidator
        (
        IBulletinCategoryRepository categoryRepository,
        IBulletinCategorySpecificationBuilder categorySpecificationBuilder

        )
    {
        _categoryRepository = categoryRepository;
        _categorySpecificationBuilder = categorySpecificationBuilder;

        RuleFor(id => id)
            .MustAsync(async (id, idField, validationContext, cancellationToken) =>
            {
                if (await IsHaveChildrenCategories(id)) { return false; }
                return true;
            }).WithMessage("This category can not be deleted because it has child categories.");
            
    }

    /// <summary>
    /// Есть ли зависящии категории.
    /// </summary>
    /// <returns></returns>
    private async Task<bool> IsHaveChildrenCategories(Guid categoryId)
    {
        var specification = _categorySpecificationBuilder
            .WhereParentId(categoryId)
            .Paginate(1, 1)
            .Build();
        IReadOnlyCollection<BulletinCategoryDto> childCategories = await _categoryRepository.FindAsync(specification);
        return childCategories.Count != 0;
    }
}
