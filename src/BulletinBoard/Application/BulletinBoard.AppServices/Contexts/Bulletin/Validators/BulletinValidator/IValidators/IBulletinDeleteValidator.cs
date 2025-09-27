using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;

/// <summary>
/// Данный валидатор принимает id удаляемой сущности объявления
/// и проверяет, есть ли зависимые от нее записи сущностей:
///     1. Строгих зависимостей пока нет.
/// </summary>
public interface IBulletinDeleteValidator : IDeleteValidator
{
}
