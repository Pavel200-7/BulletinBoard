using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using FluentValidation;
using FluentValidation.Validators;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.CustomValidators;


/// <inheritdoc/>
public class CharacteristicValidator<T> : AsyncPropertyValidator<T, Guid>
{
    /// <summary>
    /// Название валидатора
    /// </summary>
    public override string Name => "CharacteristicValidator";

    private readonly IBulletinCharacteristicRepository _characteristicRepository;

    /// <inheritdoc/>
    public CharacteristicValidator
        (
            IBulletinCharacteristicRepository characteristicRepository
        )
    {
        _characteristicRepository = characteristicRepository;
    }

    /// <summary>
    /// Валидация 
    /// </summary>
    public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid characteristicId, CancellationToken cancellation)
    {
        var characteristic = await _characteristicRepository.GetByIdAsync(characteristicId);

        if (characteristic is null)
        {
            context.MessageFormatter.AppendArgument("Error", $"A characteristic with id {characteristicId} does not exist.");
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
