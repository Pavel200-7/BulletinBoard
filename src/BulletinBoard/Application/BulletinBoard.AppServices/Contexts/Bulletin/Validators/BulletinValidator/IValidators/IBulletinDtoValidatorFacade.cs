using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.UpdateDto;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных дто работы с объявлением (как совокупности связанных сущностей)
/// </summary>
public interface IBulletinDtoValidatorFacade : IValidatorFacade<BelletinCreateDto, BelletinUpdateDtoForValidating>
{
    /// <summary>
    /// Валидировать ДТО запроса пагинации сущности.
    /// </summary>
    public Task<ValidationResult> ValidateAsync(BulletinPaginationRequestDto requersDto);

    /// <summary>
    /// Валидировать ДТО запроса пагинации сущности и выбрасывает исключение при ошибке валидации.
    /// </summary>
    public Task ValidateThrowValidationExeptionAsync(BulletinPaginationRequestDto requersDto);
}
