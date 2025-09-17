using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinRating
/// </summary>
public interface IBulletinRatingRepository
{
    /// <summary>
    /// Получить рейтинг по идентификатору.
    /// </summary>
    /// <param name="id">Id рейтинга.</param>
    /// <returns>Базовый формат данных рейтинга объявления.</returns>
    public Task<BulletinRatingDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список с рейтингами по фильтру.
    /// </summary>
    /// <param name="specification">Расширенная спецификация для фильтрации.</param>
    /// <returns>Коллекция данных базового формата данных рейтинга.</returns>
    public Task<IReadOnlyCollection<BulletinRatingDto>> FindAsync(ExtendedSpecification<BulletinRating> specification);

    /// <summary>
    /// Добавить новый рейтинг.
    /// </summary>
    /// <param name="ratingDto">Формат данных создания рейтинга объявления.</param>
    /// <returns>Базовый формат данных рейтинга объявления.</returns>
    public Task<BulletinRatingDto> CreateAsync(BulletinRatingCreateDto ratingDto);

    /// <summary>
    /// Обновить существующий рейтинг.
    /// </summary>
    /// <param name="id">Id рейтинга на обновление.</param>
    /// <param name="ratingDto">Формат данных одновления рейтинга объявления.</param>
    /// <returns>Базовый формат данных рейтинга объявления.</returns>
    public Task<BulletinRatingDto?> UpdateAsync(Guid id, BulletinRatingUpdateDto ratingDto);

    /// <summary>
    /// Удалить рейтинг по идентификатору.
    /// </summary>
    /// <param name="id">Id рейтинга на удаление.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
