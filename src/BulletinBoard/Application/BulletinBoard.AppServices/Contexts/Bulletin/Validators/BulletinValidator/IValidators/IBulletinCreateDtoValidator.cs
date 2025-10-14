using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО создания объявления (как совокупности связанных сущностей) по правилам:
/// BulletinMainCreateDto:
///     BulletinUserId:
///         1. Пользователь с таким id существует.
///         2. Пользователь с таким id не заблокирован.
///     Title:
///         1. Не пустая строка и не null.
///         2. Имеет длинну он 3 до 100.
///         3. Может хранить только русские, английские буквы нижнего, верхнего регистра, пробелы и знаки пунктуации.
///         4. Является уникальным.
///     Description:
///         1. Не пустая строка и не null.
///         2. Имеет длинну он 3 до 1000.
///         3. Может хранить только русские, английские буквы нижнего, верхнего регистра, пробелы и знаки пунктуации.
///     CategoryId:
///         1. Категория с таким id существует.
///         2. Категория с таким id является листовой.
///     Price:
///         1. Соответствует типу данных decimal.
///         2. Не является отрицательным.
/// CharacteristicComparisons:
///     1. CharacteristicId:
///         1. Характеристика существует.
///         2. Характеристика относится к той же категории, что и объявление.
///     1. CharacteristicValueId:
///         1. Значение характеристики существует.
///         2. Значение характеристики соответствует характеристике. 
/// Images:
///     1. Id:
///         1. Не null.
/// ViewsCount:
///     1. ViewsCount:
///         1. Равно 0.
/// </summary>
public interface IBulletinCreateDtoValidator : IDtoValidator<BulletinCreateDto>
{
}
