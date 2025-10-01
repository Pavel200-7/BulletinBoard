using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinMain.ReadDto;

/// <summary>
/// Базовый формат данных объявления.
/// Используется при чтении информации объявления.
/// </summary>
public class BulletinMainBulletinReadDto
{
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id пользователя - создателя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Заголовок объявления
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Id категории, к которой относится объявление
    /// </summary>}
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Цена товара или услуги, указанного в объявлении
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Время создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
