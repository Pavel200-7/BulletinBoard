using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;

/// <summary>
/// Содержит указатели на страницу.
/// </summary>
public class BulletinPagenatedPageRef
{
    /// <summary>
    /// Последний id выборки.
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Последний заголовок  выборки.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Последняя дата выборки.
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Последняя цена выборки.
    /// </summary>
    public decimal? Price { get; set; }
}
