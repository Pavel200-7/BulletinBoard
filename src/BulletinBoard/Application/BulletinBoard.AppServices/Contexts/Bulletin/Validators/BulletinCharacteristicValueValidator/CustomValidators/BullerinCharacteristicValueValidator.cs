using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;



namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.CustomValidators;

/// <inheritdoc/>
public class BullerinCharacteristicValueValidator
{
    /// <summary>
    /// Проверяет есть ли значение характеристика, относящееся 
    /// к этой характеристике с таким названием.
    /// </summary>
    public static async Task<bool> IsValueUniqueForCharacteristicAsync(
        IBulletinCharacteristicValueRepository repository,
        IBulletinCharacteristicValueSpecificationBuilder builder,
        Guid characteristicId,
        string value)
    {
        builder
            .WhereCharacteristicId(characteristicId)
            .WhereValue(value);

        var characteristics = await repository.FindAsync(builder.Build());
        return !characteristics.Any();
    }
}
