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
    /// Номер страницы
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    ///  Размер страницы
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Есть следующая страница
    /// </summary>
    public bool HasNextPage { get; set; }

    /// <summary>
    /// Указатель на следующую страницу.
    /// </summary>
    public string Next { get; set; }

    /// <summary>
    /// Указатель на предыдущую страницу.
    /// </summary>
    public string Prewious { get; set; }
}
