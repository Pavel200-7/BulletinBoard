using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;

/// <summary>
/// Формат запроса для получение погинированной выборки данных объявлений.
/// </summary>
public class BulletinPaginationRequestDto
{
    /// <summary>
    /// Количество записей на странице.
    /// 
    /// </summary>
    public int Limit { get; set; } = 10;

    /// <summary>
    /// Поле для сортировки.
    /// Заголовок - "title",
    /// Дата - "date",
    /// Цена - "price"
    /// </summary>
    public string SortBy { get; set; }

    /// <summary>
    /// Порядок сортировки.
    /// По возрастанию - "ask",
    /// По убыванию - "desk"
    /// </summary>
    public string SortOrder { get; set; }

    /// <summary>
    /// Последний идентификатор.
    /// </summary>
    public Guid? LastId { get; set; }


    /// <summary>
    /// Последняя дата.
    /// </summary>
    public DateTime? LastDate { get; set; }


    /// <summary>
    /// Последняя цена.
    /// </summary>
    public decimal? LastPrice { get; set; }


    /// <summary>
    /// Последний заголовок.
    /// </summary>
    public string? LastTitle { get; set; }


    /// <summary>
    /// Категория для фильтрации.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Минимальная цена.
    /// </summary>
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Максимальная цена.
    /// </summary>
    public decimal? MaxPrice { get; set; }

    /// <summary>
    /// Текст запроса.
    /// </summary>
    public string? SearchText { get; set; }
}
