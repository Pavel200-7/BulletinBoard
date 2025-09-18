using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.CustomValidators;

public class BullerinCharacteristicNameValidator
{
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
