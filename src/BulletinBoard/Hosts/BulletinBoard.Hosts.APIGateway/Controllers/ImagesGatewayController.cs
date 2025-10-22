using BulletinBoard.AppServices.Contexts.Apigateway.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using BulletinBoard.Contracts.DTO.Gateway.ImagesIdHolder;
using BulletinBoard.Contracts.Errors;
using BulletinBoard.Contracts.Gateway.Images;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using BulletinBoard.Hosts.APIGateway.Controllers.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using System.Security.Claims;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace BulletinBoard.Hosts.Gateway.Controllers;

/// <summary>
/// Контроллер для работы с временным хранилищем изображений.
/// </summary>
[ApiController]
[Route("api/images")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ImagesGatewayController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IInmenoryImagesIdHolderServise _idHolderServise;
    private ILogger<ImagesGatewayController> _logger;

    /// <inheritdoc/>
    public ImagesGatewayController
        (
        IHttpClientFactory httpClientFactory,
        IInmenoryImagesIdHolderServise idHolderServise,
        ILogger<ImagesGatewayController> logger
        )
    {
        _httpClientFactory = httpClientFactory;
        _idHolderServise = idHolderServise;
        _logger = logger;
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
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
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

        return await response.ToActionResult();
    }

    /// <summary>
    /// Загрузить изображение.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /api/images/upload
    ///     FormData: file
    ///     bulletinId: 019a0a68-c03a-726d-9654-ddbba2a9eb4b
    ///     isMain: true
    ///
    /// </remarks>
    /// <param name="file">Файл изображения.</param>
    /// <param name="bulletinId">Id объявления.</param>
    /// <param name="isMain">Является ли объявление главным.</param>
    /// <returns>Идентификатор загруженного изображения.</returns>
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadImage(IFormFile file, [FromForm] Guid bulletinId, [FromForm] bool isMain)
    {
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
            ImageGatewayCreateDto createDto = new ImageGatewayCreateDto()
            {
                BulletinId = bulletinId,
                IsMain = isMain
            };

            _logger.LogInformation($"Для создания изображения передается id объявления {bulletinId}");

            Guid imageId = Guid.Parse(responseContent.Trim('"'));
            var result = await CreateImageInBulletinDomain(imageId, createDto);

            if (!result.IsSuccessStatusCode)
            {
                await DeleteImageInImageDomain(imageId);
                return await result.ToActionResult();
            }
        }

        return await response.ToActionResult();
    }

    /// <summary>
    /// Создать изображение в домене объявлений.
    /// </summary>
    /// <returns></returns>
    private async Task<HttpResponseMessage> CreateImageInBulletinDomain(Guid imageId, ImageGatewayCreateDto gatewayCreateDto)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");

        BulletinImageCreateDto createDto = new BulletinImageCreateDto()
        {
            Id = imageId,
            BulletinId = gatewayCreateDto.BulletinId,
            IsMain = gatewayCreateDto.IsMain
        };

        var response = await client.PostAsJsonAsync($"/api/BulletinImage", createDto);
        return response;
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

        return await response.ToActionResult();
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
        return await response.ToActionResult();
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
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.DeleteAsync($"/api/BulletinImage/{id}");

        if (response.IsSuccessStatusCode)
        {
            await DeleteImageInImageDomain(id);
        }

        return await response.ToActionResult();
    }

    /// <summary>
    /// Удаляет изображение в домене изображений.
    /// </summary>
    /// <returns></returns>
    private async Task DeleteImageInImageDomain(Guid id)
    {
        var client = _httpClientFactory.CreateClient("ImageService");
        var response = await client.DeleteAsync($"/api/images/{id}");
    }

    /// <summary>
    /// Загрузить изображение во время создания объявления.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     Delete /api/images/while_bulletin
    ///     {
    ///         clientId: "0199d7dc-d68a-7b2d-9d0f-d7285388f189"
    ///     }
    /// </remarks>
    /// <param name="clientId">Клиентский я id изображения.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete("while_bulletin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteImageWhileBulletin([FromBody] string clientId)
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

        return await response.ToActionResult();
    }
}