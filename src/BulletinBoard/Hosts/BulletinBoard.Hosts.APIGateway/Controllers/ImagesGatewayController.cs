using BulletinBoard.AppServices.Contexts.Apigateway.Services.IServices;
using BulletinBoard.Contracts.DTO.Gateway.ImagesIdHolder;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using System.Security.Claims;

namespace BulletinBoard.Hosts.Gateway.Controllers;

/// <summary>
/// Контроллер для работы с временным хранилищем изображений.
/// </summary>
[ApiController]
[Route("api/images")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class ImagesGatewayController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IInmenoryImagesIdHolderServise _idHolderServise;

    /// <inheritdoc/>
    public ImagesGatewayController
        (
        IHttpClientFactory httpClientFactory,
        IInmenoryImagesIdHolderServise idHolderServise
        )
    {
        _httpClientFactory = httpClientFactory;
        _idHolderServise = idHolderServise;
    }

    /// <summary>
    /// Загрузить изображение во время создания объявления.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /api/images/while_bulletin
    ///     FormData: file
    ///     IsMain: true
    ///     clientImageId: "0199d7dc-d68a-7b2d-9d0f-d7285388f189"
    ///
    /// </remarks>
    /// <param name="file">Файл изображения.</param>
    /// <param name="imagesIdRequest">Информация для создания изображения.</param>
    /// <returns>Идентификатор загруженного изображения.</returns>
    [HttpPost("while_bulletin")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadImageWhileBulletin(IFormFile file, [FromForm] ImagesIdRequestDto imagesIdRequest)
    {
        string userIdSring = User.FindFirst(ClaimTypes.Sid).Value;
        Guid userId = Guid.Parse(userIdSring);

        var client = _httpClientFactory.CreateClient("ImageService");

        using var content = new MultipartFormDataContent();
        using var fileStream = file.OpenReadStream();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
        content.Add(fileContent, "file", file.FileName);

        var response = await client.PostAsync("/api/images/upload", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Guid imageId = Guid.Parse(responseContent.Trim('"'));
            var imageIdInfo = new ImagesIdDto()
            {
                Id = imageId,
                ClientImageId = imagesIdRequest.clientImageId,
                IsMain = imagesIdRequest.IsMain
            };
            _idHolderServise.Add(userId, imageIdInfo);
        }

        return StatusCode((int)response.StatusCode, content);
    }

    /// <summary>
    /// Загрузить изображение.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /api/images/upload
    ///     FormData: file
    ///
    /// </remarks>
    /// <param name="file">Файл изображения.</param>
    /// <returns>Идентификатор загруженного изображения.</returns>
    [HttpPost("upload")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var client = _httpClientFactory.CreateClient("ImageService");

        using var content = new MultipartFormDataContent();
        using var fileStream = file.OpenReadStream();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
        content.Add(fileContent, "file", file.FileName);

        var response = await client.PostAsync("/api/images/upload", content);
        var responseContent = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, content);
    }

    /// <summary>
    /// Скачать изображение.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     GET /api/images/88e6524e-b8d5-48b7-8a10-ae056325b94b/download
    ///
    /// </remarks>
    /// <param name="id">Идентификатор изображения.</param>
    /// <returns>Файл изображения.</returns>
    [HttpGet("{id}/download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadImage(Guid id)
    {
        var client = _httpClientFactory.CreateClient("ImageService");
        var response = await client.GetAsync($"/api/images/{id}/download");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsByteArrayAsync();
            var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
            var fileName = response.Content.Headers.ContentDisposition?.FileName ?? $"{id}.jpg";

            return File(content, contentType, fileName);
        }

        var errorContent = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, errorContent);
    }

    /// <summary>
    /// Получить метаинформацию об изображении.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     GET /api/images/88e6524e-b8d5-48b7-8a10-ae056325b94b/metadata
    ///
    /// </remarks>
    /// <param name="id">Идентификатор изображения.</param>
    /// <returns>Метаинформация об изображении.</returns>
    [HttpGet("{id}/metadata")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImageMetadata(Guid id)
    {
        var client = _httpClientFactory.CreateClient("ImageService");
        var response = await client.GetAsync($"/api/images/{id}/metadata");
        var content = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, content);
    }

    /// <summary>
    /// Удалить изображение.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     DELETE /api/images/88e6524e-b8d5-48b7-8a10-ae056325b94b
    ///
    /// </remarks>
    /// <param name="id">Идентификатор изображения.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        var client = _httpClientFactory.CreateClient("ImageService");
        var response = await client.DeleteAsync($"/api/images/{id}");
        var content = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, content);
    }

    /// <summary>
    /// Загрузить изображение во время создания объявления.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     Delete /api/images/while_bulletin
    ///     FormData: file
    ///     IsMain: true
    ///     clientImageId: "0199d7dc-d68a-7b2d-9d0f-d7285388f189"
    ///
    /// </remarks>
    /// <param name="clientId">Клиентский я id изображения.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete("while_bulletin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteImageWhileBulletin(string clientId)
    {
        string userIdSring = User.FindFirst(ClaimTypes.Sid).Value;
        Guid userId = Guid.Parse(userIdSring);

        var client = _httpClientFactory.CreateClient("ImageService");

        var imageInfo = _idHolderServise.GetByClientId(userId, clientId);

        if (imageInfo is null) { return Ok(false); }

        Guid imageId = imageInfo.Id;
        var response = await client.DeleteAsync($"/api/images/{imageId}");
        if (response.IsSuccessStatusCode)
        {
            _idHolderServise.Delete(userId, clientId);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, responseContent);
    }
}