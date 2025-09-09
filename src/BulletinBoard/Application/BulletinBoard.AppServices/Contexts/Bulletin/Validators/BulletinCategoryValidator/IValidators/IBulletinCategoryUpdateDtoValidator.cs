using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;

public interface IBulletinCategoryUpdateDtoValidator 
{
    public Task<ValidationResult> ValidateAsync(BulletinCategoryUpdateDto entityDto, CancellationToken cancellation = default);

}
