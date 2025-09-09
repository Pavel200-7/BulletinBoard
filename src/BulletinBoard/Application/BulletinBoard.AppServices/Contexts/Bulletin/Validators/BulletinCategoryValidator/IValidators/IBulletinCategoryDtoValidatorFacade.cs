using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;

public interface IBulletinCategoryDtoValidatorFacade
{
    public Task<ValidationResult> ValidateAsync(BulletinCategoryCreateDto entityDto);

    public Task<ValidationResult> ValidateAsync(BulletinCategoryUpdateDto entityDto);
}
