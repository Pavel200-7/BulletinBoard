using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator;

/// <inheritdoc/>
public class BulletinImageDeleteValidator : AbstractValidator<Guid>, IBulletinImageDeleteValidator
{
    /// <inheritdoc/>
    public BulletinImageDeleteValidator()
    {
        // Валидации нет. (не предполагается текущими требованиями)
    }
}
