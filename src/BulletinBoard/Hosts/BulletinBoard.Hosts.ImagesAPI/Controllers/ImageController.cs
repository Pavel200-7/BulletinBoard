using BulletinBoard.AppServices.Contexts.Images.Sercices.IServices;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.Images.Image.ReadDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с временным хранилищем изображений.
/// </summary>
[ApiController]
[Authorize]
[Route("api/images")]
public class ImagesController : ControllerBase
{
    private readonly IImageServise _imageService;

    /// <inheritdoc/>
    public ImagesController
        (
        IImageServise imageService
        )
    {
        _imageService = imageService;
    }

    /// <summary>
    /// Загрузить изображение во временное хранилище.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST api/images/upload
    ///     {
    ///        Файл
    ///     }
    ///
    /// </remarks>
    /// <param name="file">Файл изображения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор загруженного изображения.</returns>
    [HttpPost("upload")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);

        var createDto = new ImageCreateDto
        {
            Name = file.FileName,
            Content = memoryStream.ToArray(),
            ContentType = file.ContentType,
            Length = (int)file.Length
        };

        var imageId = await _imageService.UploadAsync(createDto, cancellationToken);

        return Ok(imageId);
    }

    /// <summary>
    /// Скачать изображение из временного хранилища.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     Get api/images/88e6524e-b8d5-48b7-8a10-ae056325b94b/download
    ///
    /// </remarks>
    /// <param name="id">Идентификатор изображения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Файл изображения.</returns>
    [HttpGet("{id}/download")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
    {
        var image = await _imageService.DownloadAsync(id, cancellationToken);
        return File(image.Content, image.ContentType, image.Name);
    }

    /// <summary>
    /// Получить метаинформацию об изображении.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     Get api/images/88e6524e-b8d5-48b7-8a10-ae056325b94b/metadata
    ///
    /// </remarks>
    /// <param name="id">Идентификатор изображения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Метаинформация об изображении.</returns>
    [HttpGet("{id}/metadata")]
    [ProducesResponseType(typeof(ImageInfoReadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMetadata(Guid id, CancellationToken cancellationToken)
    {
        var metadata = await _imageService.GetMetaDataAsync(id, cancellationToken);
        return Ok(metadata);
    }

    /// <summary>
    /// Удалить изображение из временного хранилища.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     Delete api/images/88e6524e-b8d5-48b7-8a10-ae056325b94b
    ///
    /// </remarks>
    /// <param name="id">Идентификатор изображения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _imageService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}