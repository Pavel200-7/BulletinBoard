using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators;

public class ParentCategoryValidator<T> : AsyncPropertyValidator<T, Guid?>
{
    public override string Name => "ParentCategoryValidator";

    private readonly IBulletinCategoryRepository _categoryRepository;

    public ParentCategoryValidator(IBulletinCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

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

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "{Error}";
}