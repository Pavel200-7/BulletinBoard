using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinRating.ReadDto;

/// <summary>
/// Базовый формат данных рейтинга объявления.
/// Используется при чтении информации объявления.
/// </summary>
public class BulletinRatingBulletinReadDto
{
    /// <summary>
    /// Id рейтинга
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id пользователя, оставившего отзыв.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Рейтинг
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Время создания.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
