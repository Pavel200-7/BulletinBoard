using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;

/// <summary>
///  Дто с пагинированными данными объявления.
/// </summary>
public class BulletinReadPagenatedDto
{
    /// <summary>
    /// Объявления
    /// </summary>
    public List<BulletinReadPagenatedItemDto> Bulletins { get; set; }

    /// <summary>
    ///  Размер страницы
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Есть следующая страница
    /// </summary>
    public bool HasNextPage { get; set; }

    /// <summary>
    /// Последний id для следующей выборки.
    /// </summary>
    public Guid? NextId { get; set; }

    /// <summary>
    /// Последняя дата для следующей выборки.
    /// </summary>
    public DateTime? NextDate { get; set; }

    /// <summary>
    /// Последняя цена для следующей выборки.
    /// </summary>
    public decimal? NextPrice { get; set; }

    /// <summary>
    /// Последний заголовок для следующей выборки.
    /// </summary>
    public string NextTitle { get; set; }

    /// <summary>
    /// Последний id для предыдущей выборки.
    /// </summary>
    public Guid? PreviousId { get; set; }

    /// <summary>
    /// Последняя дата для предыдущей выборки.
    /// </summary>
    public DateTime? PreviousDate { get; set; }

    /// <summary>
    /// Последняя цена для предыдущей выборки.
    /// </summary>
    public decimal? PreviousPrice { get; set; }

    /// <summary>
    /// Последний заголовок для предыдущей выборки.
    /// </summary>
    public string PreviousTitle { get; set; }
}
