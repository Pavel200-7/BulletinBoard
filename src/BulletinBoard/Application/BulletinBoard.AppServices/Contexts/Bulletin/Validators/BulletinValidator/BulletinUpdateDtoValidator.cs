using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator;

/// <inheritdoc/>
public class BulletinUpdateDtoValidator : AbstractValidator<BulletinUpdateDtoForValidating>, IBulletinUpdateDtoValidator
{
}
