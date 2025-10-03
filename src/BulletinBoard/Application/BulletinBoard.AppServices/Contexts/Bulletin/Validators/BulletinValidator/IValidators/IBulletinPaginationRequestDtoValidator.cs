using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО запроса пагинации объявлений по правилам:
/// Limit:
///     1. Целое неотрицательное число.
///     2. Находится в диапазоне от 5 до 50.
/// SortBy:
///     1. Задано.
///     2. Принимает одно из следующих значений: "date"/"title"/"price"
/// SortOrder:
///     1. Задано.
///     2. Принимает одно из следующих значений: "asc"/"desc"
/// LastId:
///     1. Строгих требований нет.
/// LastDate:
///     1. Строгих требований нет.
///     2. Если SortBy = "date" и задан LastId, то должен быть задан.
/// LastPrice:
///     1. Строгих требований нет.
///     2. Если SortBy = "price" и задан LastId, то должен быть задан.
/// LastTitle:
///     1. Строгих требований нет.
///     2. Если SortBy = "title" и задан LastId, то должен быть задан.
/// CategoryId:
///     1. Строгих требований нет.
/// MinPrice:
///     1. Целое неотрицательное число.
///     2. Если задан MaxPrice, то MinPrice должен быть меньше или равен MaxPrice.
/// MaxPrice:
///     1. Целое неотрицательное число.
///     2. Если задан MinPrice, то MaxPrice должен быть больше или равен MinPrice.
/// SearchText:
///     1. Длина не более 100 символов.
/// </summary>
public interface IBulletinPaginationRequestDtoValidator : IDtoValidator<BulletinPaginationRequestDto>
{
}
