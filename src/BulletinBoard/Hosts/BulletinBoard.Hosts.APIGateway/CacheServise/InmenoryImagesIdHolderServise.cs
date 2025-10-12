using BulletinBoard.AppServices.Contexts.Apigateway.Services.IServices;
using BulletinBoard.Contracts.DTO.Gateway.ImagesIdHolder;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BulletinBoard.AppServices.Contexts.Apigateway.Services;

/// <inheritdoc/>
public class InmenoryImagesIdHolderServise : IInmenoryImagesIdHolderServise
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _defaultExpiration;

    /// <inheritdoc/>
    public InmenoryImagesIdHolderServise
        (
        IMemoryCache cache
        )
    {
        _cache = cache;
        _defaultExpiration = TimeSpan.FromMinutes(30);
    }

    /// <inheritdoc/>
    public bool Add(Guid sessionId, ImagesIdDto idInfo)
    {
        string cacheKey = GetCacheKey(sessionId);

        var holder = GetOrCreateHolder(cacheKey);

        var existingImage = holder.imagesIds
            .FirstOrDefault(iId => iId.clientImageId == idInfo.clientImageId);

        if (existingImage is not null) { return false; }

        holder.imagesIds.Add(idInfo);

        _cache.Set(cacheKey, holder, new MemoryCacheEntryOptions
        {
            SlidingExpiration = _defaultExpiration
        });

        return true;
    }

    /// <inheritdoc/>
    public ImagesIdHolderDto? Get(Guid sessionId)
    {
        var cacheKey = GetCacheKey(sessionId);

        if (_cache.TryGetValue(cacheKey, out ImagesIdHolderDto holder))
        {
            return holder;
        }

        return null;
    }

    /// <inheritdoc/>
    public ImagesIdDto? GetByClientId(Guid sessionId, string clientImageId)
    {
        var holder = Get(sessionId);

        if (holder is null) { return null; }

        var image = holder.imagesIds
            .Where(iId => iId.clientImageId == clientImageId)
            .FirstOrDefault();

        return image;
    }

    /// <inheritdoc/>
    public bool Delete(Guid sessionId, string clientImageId)
    {
        var cacheKey = GetCacheKey(sessionId);

        if (!_cache.TryGetValue(cacheKey, out ImagesIdHolderDto holder))
        {
            return false;
        }

        var imageToRemove = holder!.imagesIds
            .FirstOrDefault(iId => iId.clientImageId == clientImageId);

        if (imageToRemove == null) { return false; }

        var removed = holder.imagesIds.Remove(imageToRemove);

        if (removed)
        {
            if (holder.imagesIds.Any())
            {
                _cache.Set(cacheKey, holder, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = _defaultExpiration
                });
            }
            else
            {
                _cache.Remove(cacheKey);
            }
        }

        return removed;
    }

    /// <inheritdoc/>
    public bool Clear(Guid sessionId)
    {
        var cacheKey = GetCacheKey(sessionId);
        var holder = Get(sessionId);

        if (holder is null) { return false; }

        _cache.Remove(cacheKey);

        return true;
    }

    /// <summary>
    /// Генерирует ключ для кэша
    /// </summary>
    private static string GetCacheKey(Guid sessionId)
    {
        return $"images_session_{sessionId}";
    }

    /// <summary>
    /// Получает или создает новый ImagesIdHolder для сессии
    /// </summary>
    private ImagesIdHolderDto GetOrCreateHolder(string cacheKey)
    {
        if (_cache.TryGetValue(cacheKey, out ImagesIdHolderDto holder))
        {
            return holder!;
        }

        var newHolder = new ImagesIdHolderDto();
        _cache.Set(cacheKey, newHolder, new MemoryCacheEntryOptions
        {
            SlidingExpiration = _defaultExpiration
        });

        return newHolder;
    }
}
