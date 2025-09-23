using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using FluentValidation;
using FluentValidation.Validators;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.CommonValidators;

/// <summary>
/// Класс берущий на себя задачу валидации данных по 
/// ограничениям, проверка которых требует обращения к БД.
/// Если конкретно, то он проверяет, существует ли категория с таким названием.
/// </summary>
public class UserIdValidator<T> : AsyncPropertyValidator<T, Guid>
{
    /// <summary>
    /// Название валидатора
    /// </summary>
    public override string Name => "UserIdValidator";

    private readonly IBulletinUserRepository _userRepository;

    /// <inheritdoc/>
    public UserIdValidator
        (
        IBulletinUserRepository userRepository
        )
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Валидация 
    /// </summary>
    public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid userId, CancellationToken cancellation)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            context.MessageFormatter.AppendArgument("Error", "A user with this id does not exist.");
            return false;
        }

        if (user.Blocked)
        {
            context.MessageFormatter.AppendArgument("Error", "A user with this id is blocked.");
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
