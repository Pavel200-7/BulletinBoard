using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation;
using FluentValidation.Validators;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.CustomValidators;

/// <summary>
/// Класс берущий на себя задачу валидации данных по 
/// ограничениям, проверка которых требует обращения к БД.
/// Проверяет, заголовок на уникальность.
/// </summary>
public class BulletinTitleValudator<T> : AsyncPropertyValidator<T, string>
{
    /// <summary>
    /// Название валидатора
    /// </summary>
    public override string Name => "BulletinTitleValudator";

    private readonly IBulletinMainRepository _bulletinRepository;
    private readonly IBulletinMainSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinTitleValudator
        (
        IBulletinMainRepository bulletinRepository,
        IBulletinMainSpecificationBuilder specificationBuilder
        )
    {
        _bulletinRepository = bulletinRepository;
        _specificationBuilder = specificationBuilder;
    }

    /// <summary>
    /// Валидация 
    /// </summary>
    public override async Task<bool> IsValidAsync(ValidationContext<T> context, string title, CancellationToken cancellation)
    {
        ExtendedSpecification<BulletinMain> specification = _specificationBuilder
            .WhereTitle(title)
            .Build();

        var bulletins = await _bulletinRepository.FindAsync(specification);

        if (bulletins.Any())
        {
            context.MessageFormatter.AppendArgument("Error", "A bulletin with this title already exists.");
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

