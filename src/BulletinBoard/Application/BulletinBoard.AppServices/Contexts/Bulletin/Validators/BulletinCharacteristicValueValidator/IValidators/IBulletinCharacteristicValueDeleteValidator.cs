using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;

/// <summary>
/// Данный валидатор принимает id удаляемой сущности BulletinCharacteristicValue
/// и проверяет, есть ли зависимые от нее записи сущностей:
///     1. BulletinCharacteristicComparison (связи характеристик и объявлений).
/// </summary>
public interface IBulletinCharacteristicValueDeleteValidator : IDeleteValidator
{
}
