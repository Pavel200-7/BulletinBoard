using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.CustomValidators;

/// <inheritdoc/>
public class BulletinCategoryValidator<T> : AsyncPropertyValidator<T, Guid>
{
    /// <summary>
    /// Название валидатора
    /// </summary>
    public override string Name => "BulletinCategoryValidator";

    private readonly IBulletinCategoryRepository _categoryRepository;

    /// <inheritdoc/>
    public BulletinCategoryValidator
        (
        IBulletinCategoryRepository categoryRepository
        )
    {
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Валидация 
    /// </summary>
    public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid categoryId, CancellationToken cancellation)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category is null)
        {
            context.MessageFormatter.AppendArgument("Error", "A category with this id does not exist.");
            return false;
        }

        if (!category.IsLeafy)
        {
            context.MessageFormatter.AppendArgument("Error", "This category can not be used becase it is not leafy.");
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
