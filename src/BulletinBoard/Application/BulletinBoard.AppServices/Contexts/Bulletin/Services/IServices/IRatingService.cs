using BulletinBoard.Contracts.Bulletin.BulletinRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

internal interface IRatingService
{
    /// <summary>
    /// Получить рейтинг по id.
    /// </summary>
    /// <param name="id">id рейтинга.</param>
    /// <returns>Базовый формат данных рейтинга.</returns>
    public Task<BulletinRatingDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить рейтинг.
    /// </summary>
    /// <param name="ratingDto">Формат данных создания рейтинга.</param>
    /// <returns>Базовый формат данных рейтинга.</returns>
    public Task<BulletinRatingDto> CreateAsync(BulletinRatingCreateDto ratingDto);

    /// <summary>
    /// Удалить рейтинг.
    /// </summary>
    /// <param name="id">Id рейтинга.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);

    public Task<BulletinRatingDto> SetRating(decimal newRating);

    public Task<bool> AddViewsCount();

}
