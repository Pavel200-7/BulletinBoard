using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using FluentValidation;
using FluentValidation.Validators;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.CommonValidators;

/// <summary>
///  Проверяет, существует ли объявление и заблокировано ли оно.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BulletinIdValidator<T> : AsyncPropertyValidator<T, Guid>
{
    /// <summary>
    /// Название валидатора
    /// </summary>
    public override string Name => "BulletinIdValidator";

    private readonly IBulletinMainRepository _bulletibRepository;

    /// <inheritdoc/>
    public BulletinIdValidator
        (
        IBulletinMainRepository bulletibRepository
        )
    {
        _bulletibRepository = bulletibRepository;
    }

    /// <summary>
    /// Валидация 
    /// </summary>
    public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid bulletinId, CancellationToken cancellation)
    {
        var bulletin = await _bulletibRepository.GetByIdAsync(bulletinId);

        if (bulletin is null)
        {
            context.MessageFormatter.AppendArgument("Error", "A bulletin with this id is does not exist.");
            return false;
        }

        if (bulletin.Blocked)
        {
            context.MessageFormatter.AppendArgument("Error", "A bulletin with this id is blocked.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Базовое сообщение об ошибке 
    /// </summary>
    protected override string GetDefaultMessageTemplate(string errorCode)
    => "{Error}";
}
