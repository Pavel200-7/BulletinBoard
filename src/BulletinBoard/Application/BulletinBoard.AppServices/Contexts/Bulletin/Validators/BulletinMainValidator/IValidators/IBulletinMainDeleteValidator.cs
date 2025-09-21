using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;

/// <summary>
/// Данный валидатор принимает id удаляемой сущности BulletinMain
/// и проверяет, есть ли зависимые от нее записи сущностей:
///     1. Строгих зависимостей пока нет.
/// </summary>
public interface IBulletinMainDeleteValidator : IDeleteValidator
{
}
