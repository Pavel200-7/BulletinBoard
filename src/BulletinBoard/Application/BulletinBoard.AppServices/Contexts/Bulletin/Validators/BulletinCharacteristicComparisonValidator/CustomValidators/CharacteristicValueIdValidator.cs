using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.CustomValidators;

/// <summary>
/// Проверяет поле CharacteristicValueId на условия:
///     1. Значение характеристики существует.
///     2. Значение характеристики соответствует характеристике. 
/// </summary>
public class CharacteristicValueIdValidator<T> : AsyncPropertyValidator<T, Guid>
{
    /// <summary>
    /// Название валидатора
    /// </summary>
    public override string Name => "CharacteristicValueIdValidator";

    private readonly Guid CharacteristicId;
    private readonly IBulletinCharacteristicValueRepository _characteristicValueRepository;

    /// <inheritdoc/>
    public CharacteristicValueIdValidator
        (
        Guid characteristicId,
        IBulletinCharacteristicValueRepository characteristicValueRepository
        )
    {
        CharacteristicId = characteristicId;
        _characteristicValueRepository = characteristicValueRepository;
    }

    /// <summary>
    /// Валидация 
    /// </summary>
    public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid characteristicValueId, CancellationToken cancellation)
    {
        var characteristicValue = await _characteristicValueRepository.GetByIdAsync(characteristicValueId);

        if (characteristicValue is null)
        {
            context.MessageFormatter.AppendArgument("Error", "A characteristic value with this id is does not exist.");
            return false;
        }

        if (characteristicValue.CharacteristicId != CharacteristicId)
        {
            context.MessageFormatter.AppendArgument("Error", "The characteristic value does not related to this characteristic.");
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
