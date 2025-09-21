using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonDeleteValidator : AbstractValidator<Guid>, IBulletinCharacteristicComparisonDeleteValidator
{
    /// <inheritdoc/>
    public BulletinCharacteristicComparisonDeleteValidator()
    {
        // Валидации нет. (не предполагается текущими требованиями)
    }
}
