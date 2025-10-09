using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.Images.Image.ReadDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Images.Sercices.IServices;

/// <summary>
///  сервис для работы c  временным хранилищем изображений.
/// </summary>
public interface IImageСacheServise
{
    /// <summary>
    /// Загрузка изображения.
    /// </summary>
    /// <param name="createDto">Дто загрузки изображения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Id изображения.</returns>
    public Task<Guid> UploadAsync(ImageCreateDto createDto, CancellationToken cancellationToken);

    /// <summary>
    /// Скачивание изображения.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель файла.</returns>
    public Task<ImageReadDto?> DownloadAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление изображения.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Было ли удалено изображение.</returns>
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
