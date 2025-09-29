using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinMain;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;

/// <inheritdoc/>
public class BulletinCategoryDeleteValidator : AbstractValidator<Guid>, IBulletinCategoryDeleteValidator
{
    private readonly IBulletinCategoryRepository _categoryRepository;
    private readonly IBulletinCategorySpecificationBuilder _categorySpecificationBuilder;
    private readonly IBulletinCharacteristicRepository _characteristicRepository;
    private readonly IBulletinCharacteristicSpecificationBuilder _characteristicSpecificationBuilder;
    private readonly IBulletinMainRepository _bulletinRepository;
    private readonly IBulletinMainSpecificationBuilder _bulletinSpecificationBuilder;

    /// <inheritdoc/>
    public BulletinCategoryDeleteValidator
        (
        IBulletinCategoryRepository categoryRepository,
        IBulletinCategorySpecificationBuilder categorySpecificationBuilder,
        IBulletinCharacteristicRepository characteristicRepository,
        IBulletinCharacteristicSpecificationBuilder characteristicSpecificationBuilder,
        IBulletinMainRepository bulletinRepository,
        IBulletinMainSpecificationBuilder bulletinSpecificationBuilder
        )
    {
        _categoryRepository = categoryRepository;
        _categorySpecificationBuilder = categorySpecificationBuilder;
        _characteristicRepository = characteristicRepository;
        _characteristicSpecificationBuilder = characteristicSpecificationBuilder;
        _bulletinRepository = bulletinRepository;
        _bulletinSpecificationBuilder = bulletinSpecificationBuilder;

        RuleFor(id => id)
            .MustAsync(async (id, idField, validationContext, cancellationToken) =>
            {
                if (await IsHaveChildrenCategories(id)) { return false; }
                return true;
            }).WithMessage("This category can not be deleted because it has child categories.")
            .MustAsync(async (id, idField, validationContext, cancellationToken) =>
             {
                 if (await IsHaveDependentCharacteristic(id)) { return false; }
                 return true;
             }).WithMessage("This category can not be deleted because it has dependent characteristics.")
             .MustAsync(async (id, idField, validationContext, cancellationToken) =>
             {
                 if (await IsHaveDependentBulletins(id)) { return false; }
                 return true;
             }).WithMessage("This category can not be deleted because it has dependent bulletins.");
    }

    /// <summary>
    /// Есть ли зависящии категории.
    /// </summary>
    /// <returns></returns>
    private async Task<bool> IsHaveChildrenCategories(Guid categoryId)
    {
        var specification = _categorySpecificationBuilder
            .WhereParentId(categoryId)
            .Paginate(1, 1)
            .Build();
        IReadOnlyCollection<BulletinCategoryDto> childCategories = await _categoryRepository.FindAsync(specification);
        return childCategories.Count != 0;
    }

    /// <summary>
    /// Есть ли зависящии объявления характеристики.
    /// </summary>
    private async Task<bool> IsHaveDependentCharacteristic(Guid categoryId)
    {
        var specification = _characteristicSpecificationBuilder
            .WhereCategoryId(categoryId)
            .Paginate(1, 1)
            .Build();

        IReadOnlyCollection<BulletinCharacteristicDto> dependentCharacteristic = await _characteristicRepository.FindAsync(specification);
        return dependentCharacteristic.Count != 0;
    }

    /// <summary>
    /// Есть ли зависящии объявления.
    /// </summary>
    private async Task<bool> IsHaveDependentBulletins(Guid categoryId)
    {
        var specification = _bulletinSpecificationBuilder
            .WhereCategoryId(categoryId)
            .Paginate(1, 1)
            .Build();

        IReadOnlyCollection<BulletinMainDto> dependentBulletin = await _bulletinRepository.FindAsync(specification);
        return dependentBulletin.Count != 0;
    }
}
