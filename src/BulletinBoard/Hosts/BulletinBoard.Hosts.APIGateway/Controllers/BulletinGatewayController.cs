using BulletinBoard.AppServices.Contexts.Apigateway.Services.IServices;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.UpdateDto;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace BulletinBoard.Hosts.Gateway.Controllers
{
    /// <summary>
    /// Контроллер для работы с объявлениями.
    /// </summary>
    [ApiController]
    [Route("api/bulletin")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
    public class BulletinGatewayController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IInmenoryImagesIdHolderServise _idHolderServise;

        /// <inheritdoc/>
        public BulletinGatewayController
            (
            IHttpClientFactory httpClientFactory,
            IInmenoryImagesIdHolderServise idHolderServise
            )
        {
            _httpClientFactory = httpClientFactory;
            _idHolderServise = idHolderServise;
        }

        /// <summary>
        /// Получить значение объявление по id.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    GET /api/bulletin/01992529-1ec8-766f-a03d-a7ac4f0996b9
        ///
        /// </remarks>
        /// <param name="id">Идентификатор объявления.</param>
        /// <returns>Данные объявления.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBulletin(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync($"/api/Bulletin/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonSerializer.Deserialize<JsonElement>(content));
        }

        /// <summary>
        /// Получить значение объявление по id (Без избыточных идентификаторов).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    GET /api/bulletin/01992529-1ec8-766f-a03d-a7ac4f0996b9/single
        ///
        /// </remarks>
        /// <param name="id">Идентификатор объявления.</param>
        /// <returns>Данные объявления.</returns>
        [HttpGet("{id}/single")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBulletinSingle(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync($"/api/Bulletin/{id}/Single");
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonSerializer.Deserialize<JsonElement>(content));
        }

        /// <summary>
        /// Получить отсортированную и отфильтрованную выборку объявлений (страницу).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    POST /api/bulletin/search
        ///    {
        ///         "limit": 0,
        ///         "sortBy": "Date",
        ///         "sortOrder": "string",
        ///         "lastId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "lastDate": "2025-10-01T17:43:47.431Z",
        ///         "lastPrice": 0,
        ///         "lastTitle": "Заголовок 7712",
        ///         "categoryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "minPrice": 0,
        ///         "maxPrice": 1000,
        ///         "searchText": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Запрос на получение выборки.</param>
        /// <returns>Коллекция данных объявления.</returns>
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchBulletins([FromBody] BulletinPaginationRequestDto request)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PostAsJsonAsync("/api/Bulletin/Bulletins", request);
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonSerializer.Deserialize<JsonElement>(content));
        }

        /// <summary>
        /// Создать объявление.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    POST /api/bulletin
        ///    {
        ///         "bulletinMain": 
        ///         {
        ///             "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "title": "string",
        ///             "description": "string",
        ///             "categoryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "price": 0
        ///         },
        ///         "characteristicComparisons": 
        ///         [
        ///             {
        ///                 "characteristicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "characteristicValueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///             }
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <param name="bulletin">Формат данных создания нового объявления.</param>
        /// <returns>Id объявления.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBulletin([FromBody] BulletinCreateDtoRequest bulletin)
        {
            string userIdSring = User.FindFirst(ClaimTypes.Sid).Value;
            Guid userId = Guid.Parse(userIdSring);

            var bulletinCreateDto = new BulletinCreateDtoController(bulletin);
            var imagesCreateDto = GetImagesIdList(userId);
            bulletinCreateDto.AddImages(imagesCreateDto);

            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PostAsJsonAsync("/api/Bulletin", bulletinCreateDto);
            
            if (response.IsSuccessStatusCode)
            {
                _idHolderServise.Clear(userId);
            }

            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonSerializer.Deserialize<JsonElement>(content));
        }

        /// <summary>
        /// Получить список id изображений, которые были добавлены как часть объявления.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        private List<BulletinImageCreateDtoWhileBulletinCreating> GetImagesIdList(Guid sessionId)
        {
            var imagesCreateDto = new List<BulletinImageCreateDtoWhileBulletinCreating>();

            var idHolder = _idHolderServise.Get(sessionId);
            if (idHolder is not null)
            {
                idHolder.imagesIds.ForEach(id =>
                {
                    var imageInfo = new BulletinImageCreateDtoWhileBulletinCreating()
                    {
                        Id = id.Id,
                        IsMain = id.IsMain,
                    };
                    imagesCreateDto.Add(imageInfo);
                });

            }

            return imagesCreateDto;
        }

        /// <summary>
        /// Обновить объявление.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    PUT /api/bulletin/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///    {
        ///         "title": "Заголовок объявления.",
        ///         "description": "Описание объявления.",
        ///         "price": 1000
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Id объявления.</param>
        /// <param name="bulletin">Данные обновления объявления.</param>
        /// <returns>Id объявления.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBulletin(Guid id, [FromBody] BulletinMainUpdateDto bulletin)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PutAsJsonAsync($"/api/Bulletin/{id}", bulletin);
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonSerializer.Deserialize<JsonElement>(content));
        }

        /// <summary>
        /// Удалить объявление.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    DELETE /api/bulletin/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///
        /// </remarks>
        /// <param name="id">Id объявления.</param>
        /// <returns>Результат удаления.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBulletin(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.DeleteAsync($"/api/Bulletin/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonSerializer.Deserialize<JsonElement>(content));
        }
    }
}