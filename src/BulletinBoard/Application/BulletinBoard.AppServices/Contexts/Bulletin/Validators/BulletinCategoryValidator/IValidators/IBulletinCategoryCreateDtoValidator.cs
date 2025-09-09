using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators
{
    public interface IBulletinCategoryCreateDtoValidator
    {
        public Task<ValidationResult> ValidateAsync(BulletinCategoryCreateDto entityDto, CancellationToken cancellation = default);
    }
}
