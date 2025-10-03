using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
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
    /// Последний заголовок для следующей выборки.
    /// </summary>
    public string? NextTitle { get; set; }

    /// <summary>
    /// Последняя дата для следующей выборки.
    /// </summary>
    public DateTime? NextDate { get; set; }

    /// <summary>
    /// Последняя цена для следующей выборки.
    /// </summary>
    public decimal? NextPrice { get; set; }

    /// <summary>
    /// Последний id для предыдущей выборки.
    /// </summary>
    public Guid? PreviousId { get; set; }

    /// <summary>
    /// Последний заголовок для предыдущей выборки.
    /// </summary>
    public string? PreviousTitle { get; set; }

    /// <summary>
    /// Последняя дата для предыдущей выборки.
    /// </summary>
    public DateTime? PreviousDate { get; set; }

    /// <summary>
    /// Последняя цена для предыдущей выборки.
    /// </summary>
    public decimal? PreviousPrice { get; set; }


    /// <summary>
    /// Конструктор ответа - пагинированной выборки объяв
    /// </summary>
    /// <param name="bulletins"></param>
    /// <param name="requestDto"></param>
    public BulletinReadPagenatedDto(List<BulletinReadPagenatedItemDto> bulletins, BulletinPaginationRequestDto requestDto)
    {
        Bulletins = bulletins;

        if (bulletins.Count() == requestDto.Limit + 1)
        {
            HasNextPage = true;
            BulletinReadPagenatedItemDto nextPageItem = PopLastBulletinItem();
            SetNextPageRefs(nextPageItem, requestDto);
        }

        SetLastPageRefs(requestDto);
    }

    /// <summary>
    /// Получить и удалить последний элемент списка объявлений.
    /// </summary>
    /// <returns>Последний элемент списка объявлений</returns>
    public BulletinReadPagenatedItemDto PopLastBulletinItem()
    {
        int lastIndex = Bulletins.Count() - 1;
        var lastItem = Bulletins[lastIndex];
        Bulletins.RemoveAt(lastIndex);
        return lastItem;
    }

    /// <summary>
    /// Установить данные поиска слудующей страницы.
    /// </summary>
    /// <param name="nextPageItem">Первый элемент следующей страницы.</param>
    /// <param name="requestDto">Данные запроса пагинации, полученные от клиента.</param>
    public void SetNextPageRefs(BulletinReadPagenatedItemDto nextPageItem, BulletinPaginationRequestDto requestDto)
    {
        NextId = nextPageItem.Main.Id;

        switch (requestDto.SortBy)
        {
            case "title":
                NextTitle = nextPageItem.Main.Title;
                break;
            case "date":
                NextDate = nextPageItem.Main.CreatedAt;
                break;
            case "price":
                NextPrice = nextPageItem.Main.Price;
                break;
        }
    }

    /// <summary>
    /// Установить данные поиска текущей страницы (которая станет предыдушей при переходе на следующую, логично).
    /// </summary>
    /// <param name="requestDto">Данные запроса пагинации, полученные от клиента.</param>
    public void SetLastPageRefs(BulletinPaginationRequestDto requestDto)
    {
        PreviousId = requestDto.LastId;

        switch (requestDto.SortBy)
        {
            case "title":
                PreviousTitle = requestDto.LastTitle;
                break;
            case "date":
                PreviousDate = requestDto.LastDate;
                break;
            case "price":
                PreviousPrice = requestDto.LastPrice;
                break;
        }
    }
}
