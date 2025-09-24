using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinImage.ForValidating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО обновления изображения объявления по правилам:
/// Name:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 255.
///     3. Может хранить только русские, английские буквы нижнего, верхнего регистра, пробелы и знаки пунктуации.
/// </summary>
public interface IBulletinImageUpdateDtoValidator : IDtoValidator<BulletinImageUpdateDtoForValidating>
{
}
