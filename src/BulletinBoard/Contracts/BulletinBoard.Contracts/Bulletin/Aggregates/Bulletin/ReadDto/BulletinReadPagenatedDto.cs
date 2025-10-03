using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
    /// Данные поиска слудующей страницы.
    /// </summary>
    public BulletinPagenatedPageRef? Next { get; set; } = null;

    /// <summary>
    /// Данные поиска предыдущей страницы.
    /// </summary>
    public BulletinPagenatedPageRef? Previous { get; set; } = null;


    /// <summary>
    /// Конструктор ответа - пагинированной выборки объявления.
    /// </summary>
    /// <param name="bulletins"></param>
    /// <param name="requestDto"></param>
    public BulletinReadPagenatedDto(List<BulletinReadPagenatedItemDto> bulletins, BulletinPaginationRequestDto requestDto)
    {
        Bulletins = bulletins;

        HasNextPage = bulletins.Count > requestDto.Limit;

        if (HasNextPage)
        {
            BulletinReadPagenatedItemDto nextPageItem = PopLastBulletinItem();
            SetNextPageRefs(nextPageItem, requestDto.SortBy);
        }

        SetPreviousPageRefs(requestDto);
        PageSize = Bulletins.Count();
    }


    /// <summary>
    /// Конструктор ответа - пагинированной выборки объявления для сериализации.
    /// </summary>
    public BulletinReadPagenatedDto() { }


    /// <summary>
    /// Получить и удалить последний элемент списка объявлений.
    /// </summary>
    /// <returns>Последний элемент списка объявлений</returns>
    public BulletinReadPagenatedItemDto PopLastBulletinItem()
    {
        var lastItem = Bulletins[^1];
        Bulletins.RemoveAt(Bulletins.Count() - 1);
        return lastItem;
    }

    /// <summary>
    /// Установить данные поиска слудующей страницы.
    /// </summary>
    /// <param name="nextPageItem">Первый элемент следующей страницы.</param>
    /// <param name="sortBy">Поле сортировки.</param>
    public void SetNextPageRefs(BulletinReadPagenatedItemDto nextPageItem, string sortBy)
    {
        Next = new();
        Next.Id = nextPageItem.Main.Id;

        switch (sortBy)
        {
            case "title":
                Next.Title = nextPageItem.Main.Title;
                break;
            case "date":
                Next.Date = nextPageItem.Main.CreatedAt;
                break;
            case "price":
                Next.Price = nextPageItem.Main.Price;
                break;
        }
    }

    /// <summary>
    /// Установить данные поиска текущей страницы (которая станет предыдушей при переходе на следующую, логично).
    /// </summary>
    /// <param name="requestDto">Данные запроса пагинации, полученные от клиента.</param>
    public void SetPreviousPageRefs(BulletinPaginationRequestDto requestDto)
    {
        if (requestDto.LastId is null) { return; }

        Previous = new();
        Previous.Id = requestDto.LastId;

        switch (requestDto.SortBy)
        {
            case "title":
                Previous.Title = requestDto.LastTitle;
                break;
            case "date":
                Previous.Date = requestDto.LastDate;
                break;
            case "price":
                Previous.Price = requestDto.LastPrice;
                break;
        }
    }
}

