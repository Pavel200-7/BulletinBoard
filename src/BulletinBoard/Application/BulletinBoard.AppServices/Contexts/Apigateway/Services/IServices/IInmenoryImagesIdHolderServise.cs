using BulletinBoard.Contracts.DTO.Gateway.ImagesIdHolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Apigateway.Services.IServices;

/// <summary>
/// Интерфейс для временного сохранения id изображений,
/// добавляемых в ходе создания объявления.
/// </summary>
public interface IInmenoryImagesIdHolderServise
{
    /// <summary>
    /// Добавить в список хранилища, привязанного к сессии пользователя
    /// id загруженного изображения.
    /// </summary>
    /// <param name="sessionId">id сессии пользователя.</param>
    /// <param name="idInfo">информация о загруженном изображении.</param>
    /// <returns>Был ли загружен id.</returns>
    public bool Add(Guid sessionId, ImagesIdDto idInfo);

    /// <summary>
    /// Получить список всех идентификаторов, привяззанных к 
    /// текущей сессии.
    /// </summary>
    /// <param name="sessionId">id сессии пользователя.</param>
    /// <returns>Список id изображений.</returns>
    public ImagesIdHolderDto? Get(Guid sessionId);

    /// <summary>
    /// Получить список всех идентификаторов, привяззанных к 
    /// текущей сессии.
    /// </summary>
    /// <param name="sessionId">id сессии пользователя.</param>
    /// <param name="clientImageId">Клиентский id.</param>
    /// <returns>id изображения.</returns>
    public ImagesIdDto? GetByClientId(Guid sessionId, string clientImageId);

    /// <summary>
    /// Удвлить по пользовательскому id.
    /// </summary>
    /// <param name="sessionId">id сессии пользователя.</param>
    /// <param name="clientImageId">Клиентский id.</param>
    /// <returns>Был ли удален id.</returns>
    public bool Delete(Guid sessionId, string clientImageId);

    /// <summary>
    /// Отчистить список id, привязанный к текущей сессии.
    /// </summary>
    /// <param name="sessionId">id сессии пользователя.</param>
    /// <returns>были ли удалены все id.</returns>
    public bool Clear(Guid sessionId);
}
