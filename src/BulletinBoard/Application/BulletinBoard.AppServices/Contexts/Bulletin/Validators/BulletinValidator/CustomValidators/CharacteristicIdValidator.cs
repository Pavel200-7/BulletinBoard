using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.CustomValidators;

/// <summary>
/// Проверяет поле CharacteristicId на условия:
///     1. Характеристика существует.
///     2. Характеристика относится к той же категории, что и объявление.
/// </summary>
public class CharacteristicIdValidator<T> : AsyncPropertyValidator<T, Guid>
{
    /// <summary>
    /// Название валидатора
    /// </summary>
    public override string Name => "CharacteristicIdValidator";

    private readonly Guid BulletinCategoryId;
    private readonly IBulletinCharacteristicRepository _characteristicRepository;

    /// <inheritdoc/>
    public CharacteristicIdValidator
        (
        Guid bulletinCategoryId,
        IBulletinCharacteristicRepository characteristicRepository
        )
    {
        BulletinCategoryId = bulletinCategoryId;
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
            context.MessageFormatter.AppendArgument("Error", "A characteristic with this id is does not exist.");
            return false;
        }

        if (BulletinCategoryId != characteristic.CategoryId)
        {
            context.MessageFormatter.AppendArgument("Error", "The characteristic and the bulletin belong to different categories.");
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
