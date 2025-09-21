using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator;

/// <inheritdoc/>
public class BulletinUserDeleteValidator : AbstractValidator<Guid>, IBulletinUserDeleteValidator
{
    private readonly IBulletinMainRepository _bulletinRepository;
    private readonly IBulletinMainSpecificationBuilder _bulletinSpecificationBuilder;

    /// <inheritdoc/>
    public BulletinUserDeleteValidator
        (
        IBulletinMainRepository bulletinRepository,
        IBulletinMainSpecificationBuilder bulletinSpecificationBuilder
        )
    {
        _bulletinRepository = bulletinRepository;
        _bulletinSpecificationBuilder = bulletinSpecificationBuilder;

        RuleFor(id => id)
            .MustAsync(async (id, idField, validatinContext, cancellationToken) =>
            {
                if (await IsHaveDependentBulletins(id)) { return false; }
                return true;
            }).WithMessage("This user can not be deleted because it has dependent bulletins.");
    }

    private async Task<bool> IsHaveDependentBulletins(Guid id)
    {
        var specification = _bulletinSpecificationBuilder
            .WhereUserId(id)
            .Paginate(1, 1)
            .Build();

        IReadOnlyCollection<BulletinMainDto> bulletins = await _bulletinRepository.FindAsync(specification);
        return bulletins.Count != 0;
    }
}
