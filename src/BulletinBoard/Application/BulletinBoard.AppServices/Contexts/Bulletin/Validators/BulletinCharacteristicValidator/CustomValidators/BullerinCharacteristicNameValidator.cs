using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.CustomValidators;

/// <inheritdoc/>
public class BullerinCharacteristicNameValidator
{
    /// <summary>
    /// Проверяет есть ли характеристика, относящаяся 
    /// к этой категории с таким названием.
    /// </summary>
    public static async Task<bool> IsNameUniqueForCategoryAsync(
        IBulletinCharacteristicRepository repository,
        IBulletinCharacteristicSpecificationBuilder builder,
        Guid categoryId,
        string name)
    {
        builder
            .WhereCategoryId(categoryId)
            .WhereName(name);

        var characteristics = await repository.FindAsync(builder.Build());
        return !characteristics.Any();
    }

}
