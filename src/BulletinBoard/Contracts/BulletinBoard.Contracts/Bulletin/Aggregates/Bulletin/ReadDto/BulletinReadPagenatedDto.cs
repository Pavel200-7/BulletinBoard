using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;

public class BulletinReadPagenatedDto
{
    /// <summary>
    /// Объявления
    /// </summary>
    public List<string> Bulletins { get; set; }

    /// <summary>
    /// Номер страницы
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    ///  Размер страницы
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Всего записей
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Всего страниц
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Есть следующая страница
    /// </summary>
    public bool HasNextPage { get; set; }

    /// <summary>
    /// Есть предыдущая страница
    /// </summary>
    public bool HasPreviousPage { get; set; }

}
