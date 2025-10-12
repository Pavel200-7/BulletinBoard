// BulletinBoard.Hosts.Gateway/Controllers/ImagesGatewayController.cs
using BulletinBoard.AppServices.Contexts.Apigateway.Services.IServices;
using BulletinBoard.Contracts.DTO.Gateway.ImagesIdHolder;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesGatewayController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IInmenoryImagesIdHolderServise _idHolderServise;

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
        ///     POST /api/ImagesGateway/upload
        ///     {
        ///         FormData: file,
        ///         clientImageId: "04d28d48-8723-4b96-a070-5155f73545c4"
        ///     }
        ///     
        ///
        /// </remarks>
        /// <param name="file">Файл изображения.</param>
        /// <param name="imagesIdRequest">Информация для создания изображения.</param>

        /// <returns>Идентификатор загруженного изображения.</returns>
        [HttpPost("upload_with_bulletin")]
        public async Task<IActionResult> UploadImage(IFormFile file, ImagesIdRequestDto imagesIdRequest)
        {
            // TODO текущий id сессии фейковый
             Guid sessionId = Guid.NewGuid();


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
                Guid imageId = Guid.Parse(responseContent.Trim('"')); // Убираем кавычки если есть
                var imageIdInfo = new ImagesIdDto()
                {
                    Id = imageId,
                    clientImageId = imagesIdRequest.clientImageId,
                    IsMain = imagesIdRequest.IsMain
                };
                _idHolderServise.Add(sessionId, imageIdInfo);
            }

            return Content(responseContent, "application/json");
        }

        /// <summary>
        /// Загрузить изображение во временное хранилище.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /api/ImagesGateway/upload
        ///     FormData: file
        ///
        /// </remarks>
        /// <param name="file">Файл изображения.</param>
        /// <returns>Идентификатор загруженного изображения.</returns>
        [HttpPost("upload")]
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
            return Content(responseContent, "application/json");
        }

        /// <summary>
        /// Скачать изображение из временного хранилища.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/ImagesGateway/88e6524e-b8d5-48b7-8a10-ae056325b94b/download
        ///
        /// </remarks>
        /// <param name="id">Идентификатор изображения.</param>
        /// <returns>Файл изображения.</returns>
        [HttpGet("{id}/download")]
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
            return Content(errorContent, "application/json");
        }

        /// <summary>
        /// Получить метаинформацию об изображении.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/ImagesGateway/88e6524e-b8d5-48b7-8a10-ae056325b94b/metadata
        ///
        /// </remarks>
        /// <param name="id">Идентификатор изображения.</param>
        /// <returns>Метаинформация об изображении.</returns>
        [HttpGet("{id}/metadata")]
        public async Task<IActionResult> GetImageMetadata(Guid id)
        {
            var client = _httpClientFactory.CreateClient("ImageService");
            var response = await client.GetAsync($"/api/images/{id}/metadata");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Удалить изображение из временного хранилища.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE /api/ImagesGateway/88e6524e-b8d5-48b7-8a10-ae056325b94b
        ///
        /// </remarks>
        /// <param name="id">Идентификатор изображения.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            var client = _httpClientFactory.CreateClient("ImageService");
            var response = await client.DeleteAsync($"/api/images/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
    }
}