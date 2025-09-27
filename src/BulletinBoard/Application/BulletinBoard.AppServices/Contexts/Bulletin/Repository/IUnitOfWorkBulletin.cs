using BulletinBoard.AppServices.Repository;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Класс для работы с транзакциями и репозиториями домена Bulletin.
/// </summary>
public interface IUnitOfWorkBulletin : IUnitOfWork
{
    /// <summary>
    /// Репозиторий категорий.
    /// </summary>
    public IBulletinCategoryRepository _categoryRepository { get; }

    /// <summary>
    /// Репозиторий сопоставления характеристик с объявлениями.
    /// </summary>
    public IBulletinCharacteristicComparisonRepository _comparisonRepository { get; }

    /// <summary>
    /// Репозиторий характеристик.
    /// </summary>
    public IBulletinCharacteristicRepository _characteristicRepository { get; }

    /// <summary>
    /// Репозиторий значений характеристик.
    /// </summary>
    public IBulletinCharacteristicValueRepository _characteristicValueRepository { get; }

    /// <summary>
    /// Репозиторий изображений (ссылок на них. см в домене).
    /// </summary>
    public IBulletinImageRepository _imageRepository { get; }

    /// <summary>
    /// Репозиторий объявлений.
    /// </summary>
    public IBulletinMainRepository _mainRepository { get; }

    /// <summary>
    /// Репозиторий рейтингов.
    /// </summary>
    public IBulletinRatingRepository _ratingRepository { get; }

    /// <summary>
    /// Репозиторий пользоватеей.
    /// </summary>
    public IBulletinUserRepository _userRepository { get; }

    /// <summary>
    /// Репозиторий счетчика просмотров.
    /// </summary>
    public IBulletinViewsCountRepository _viewsCountRepository { get; }
}
