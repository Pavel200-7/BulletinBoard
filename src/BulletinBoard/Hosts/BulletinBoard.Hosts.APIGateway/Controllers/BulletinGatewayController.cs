// BulletinBoard.Hosts.Gateway/Controllers/BulletinGatewayController.cs
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BulletinBoard.Hosts.Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BulletinGatewayController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BulletinGatewayController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Получение категории по id.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    GET /api/BulletinGateway/categories/01992529-1ec8-766f-a03d-a7ac4f0996b9
        ///
        /// </remarks>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>Базовый формат данных категории.</returns>
        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync($"/api/BulletinCategory/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Создание категории.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /api/BulletinGateway/categories
        ///     {
        ///        "parentCategoryId" : "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///        "categoryName" : "Категория 1",
        ///        "isLeafy": false
        ///     }
        ///
        /// </remarks>
        /// <param name="category">Формат данных создания категории.</param>
        /// <returns>Базовый формат данных категории.</returns>
        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] JsonElement category)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PostAsJsonAsync("/api/BulletinCategory", category);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Изменение категории.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /api/BulletinGateway/categories/01992a7c-71f5-7081-9311-44884bac8d58
        ///     {
        ///        "parentCategoryId" : "0199252f-2d8c-7892-ada3-91600c780739",
        ///        "categoryName" : "Категория 5",
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Идентификатор категории, которую нужно обновить.</param>
        /// <param name="category">Формат данных изменения категории.</param>
        /// <returns>Базовый формат данных категории.</returns>
        [HttpPut("categories/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] JsonElement category)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PutAsJsonAsync($"/api/BulletinCategory/{id}", category);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Удалить категории.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE /api/BulletinGateway/categories/01992529-1ec8-766f-a03d-a7ac4f0996b9
        ///
        /// </remarks>
        /// <param name="id">Идентификатор категории, которую нужно удалить.</param>
        /// <returns>true если все прошло успешно.</returns>
        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.DeleteAsync($"/api/BulletinCategory/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Получение всех категорий в их правильном иерархическом виде.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/BulletinGateway/categories
        ///
        /// </remarks>
        /// <returns>Формат данных для вывода всех категорий в их правильном иерархическом виде.</returns>
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync("/api/BulletinCategory");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Получение одной категории в виде древовидной струкруры от самого корня.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/BulletinGateway/categories/01992529-1ec8-766f-a03d-a7ac4f0996b9/singlechain
        ///
        /// </remarks>
        /// <returns>Формат данных для вывода одной карегории в виде древовидной струкруры от самого корня.</returns>
        [HttpGet("categories/{id}/singlechain")]
        public async Task<IActionResult> GetCategorySingleChain(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync($"/api/BulletinCategory/{id}/SingleChain");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Получить значение объявление по id.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    GET /api/BulletinGateway/bulletins/01992529-1ec8-766f-a03d-a7ac4f0996b9
        ///
        /// </remarks>
        /// <param name="id">Идентификатор объявления.</param>
        /// <returns>Данные объявления.</returns>
        [HttpGet("bulletins/{id}")]
        public async Task<IActionResult> GetBulletin(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync($"/api/Bulletin/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Получить значение объявление по id (Без избыточных идентификаторов).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    GET /api/BulletinGateway/bulletins/01992529-1ec8-766f-a03d-a7ac4f0996b9/single
        ///
        /// </remarks>
        /// <param name="id">Идентификатор объявления.</param>
        /// <returns>Данные объявления.</returns>
        [HttpGet("bulletins/{id}/single")]
        public async Task<IActionResult> GetBulletinSingle(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync($"/api/Bulletin/{id}/Single");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Получить отсортированную и отфильтрованную выборку объявлений (страницу).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    POST /api/BulletinGateway/bulletins/search
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
        [HttpPost("bulletins/search")]
        public async Task<IActionResult> SearchBulletins([FromBody] JsonElement request)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PostAsJsonAsync("/api/Bulletin/Bulletins", request);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Создать объявление.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    POST /api/BulletinGateway/bulletins
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
        [HttpPost("bulletins")]
        public async Task<IActionResult> CreateBulletin([FromBody] JsonElement bulletin)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PostAsJsonAsync("/api/Bulletin", bulletin);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Обновить объявление.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    PUT /api/BulletinGateway/bulletins/3fa85f64-5717-4562-b3fc-2c963f66afa6
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
        [HttpPut("bulletins/{id}")]
        public async Task<IActionResult> UpdateBulletin(Guid id, [FromBody] JsonElement bulletin)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PutAsJsonAsync($"/api/Bulletin/{id}", bulletin);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Удалить объявление.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    DELETE /api/BulletinGateway/bulletins/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///
        /// </remarks>
        /// <param name="id">Id объявления.</param>
        /// <returns>Результат удаления.</returns>
        [HttpDelete("bulletins/{id}")]
        public async Task<IActionResult> DeleteBulletin(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.DeleteAsync($"/api/Bulletin/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Получение характеристики по id.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    GET /api/BulletinGateway/characteristics/01992529-1ec8-766f-a03d-a7ac4f0996b9
        ///
        /// </remarks>
        /// <param name="id">Идентификатор характеристики.</param>
        /// <returns>Базовый формат данных характеристики.</returns>
        [HttpGet("characteristics/{id}")]
        public async Task<IActionResult> GetCharacteristic(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync($"/api/BulletinCharacteristic/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Создание характеристики.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /api/BulletinGateway/characteristics
        ///     {
        ///        "name": "Характеристика 1",
        ///        "categoryId": "01995805-ca8c-73c2-a120-4315a88208e8"
        ///     }
        ///
        /// </remarks>
        /// <param name="characteristic">Формат данных создания характеристики.</param>
        /// <returns>Базовый формат данных характеристики.</returns>
        [HttpPost("characteristics")]
        public async Task<IActionResult> CreateCharacteristic([FromBody] JsonElement characteristic)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PostAsJsonAsync("/api/BulletinCharacteristic", characteristic);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Обновить характеристику.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /api/BulletinGateway/characteristics/01995805-ca8c-73c2-a120-4315a88208e8
        ///     {
        ///        "name": "Характеристика 1",
        ///     }
        ///
        /// </remarks>
        /// <param name="id">id характеристики.</param>
        /// <param name="characteristic">Формат данных обновления характеристики.</param>
        /// <returns>Базовый формат данных характеристики.</returns>
        [HttpPut("characteristics/{id}")]
        public async Task<IActionResult> UpdateCharacteristic(Guid id, [FromBody] JsonElement characteristic)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PutAsJsonAsync($"/api/BulletinCharacteristic/{id}", characteristic);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Удалить характеристику.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE /api/BulletinGateway/characteristics/01992529-1ec8-766f-a03d-a7ac4f0996b9
        ///
        /// </remarks>
        /// <param name="id">Идентификатор характеристики.</param>
        /// <returns>Результат удаления.</returns>
        [HttpDelete("characteristics/{id}")]
        public async Task<IActionResult> DeleteCharacteristic(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.DeleteAsync($"/api/BulletinCharacteristic/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Получить значение характеристики по id.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    GET /api/BulletinGateway/characteristic-values/01992529-1ec8-766f-a03d-a7ac4f0996b9
        ///
        /// </remarks>
        /// <param name="id">Идентификатор значения характеристики.</param>
        /// <returns>Базовый формат данных значения характеристики.</returns>
        [HttpGet("characteristic-values/{id}")]
        public async Task<IActionResult> GetCharacteristicValue(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync($"/api/BulletinCharacteristicValue/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Создать новое значение характеристики.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    POST /api/BulletinGateway/characteristic-values
        ///    {
        ///         "characteristicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "value": "Значени хорактеристики"
        ///     }
        ///
        /// </remarks>
        /// <param name="characteristicValue">Формат данных создания характеристики.</param>
        /// <returns>Формат данных значения характеристики.</returns>
        [HttpPost("characteristic-values")]
        public async Task<IActionResult> CreateCharacteristicValue([FromBody] JsonElement characteristicValue)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PostAsJsonAsync("/api/BulletinCharacteristicValue", characteristicValue);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Обновить значение характеристики по id.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    PUT /api/BulletinGateway/characteristic-values/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///    {
        ///         "value": "Значени хорактеристики"
        ///    }
        ///
        /// </remarks>
        /// <param name="id">id значения характеристики.</param>
        /// <param name="characteristicValue">Формат данных обновления значения характеристики.</param>
        /// <returns>Формат данных значения характеристики.</returns>
        [HttpPut("characteristic-values/{id}")]
        public async Task<IActionResult> UpdateCharacteristicValue(Guid id, [FromBody] JsonElement characteristicValue)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PutAsJsonAsync($"/api/BulletinCharacteristicValue/{id}", characteristicValue);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Удалить значение характеристики по id.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    DELETE /api/BulletinGateway/characteristic-values/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///
        /// </remarks>
        /// <param name="id">id значения характеристики.</param>
        /// <returns>Результат удаления.</returns>
        [HttpDelete("characteristic-values/{id}")]
        public async Task<IActionResult> DeleteCharacteristicValue(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.DeleteAsync($"/api/BulletinCharacteristicValue/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Получить все значения по id характеристики.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    GET /api/BulletinGateway/characteristic-values/byCharacteristic/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///
        /// </remarks>
        /// <param name="characteristicId">id характеристики.</param>
        /// <returns>Колкекция - результат отбора.</returns>
        [HttpGet("characteristic-values/byCharacteristic/{characteristicId}")]
        public async Task<IActionResult> GetCharacteristicValuesByCharacteristic(Guid characteristicId)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync($"/api/BulletinCharacteristicValue/byCharacteristic/{characteristicId}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Получение сопоставления характеристики с объявлением по id.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///    GET /api/BulletinGateway/characteristic-comparisons/01992529-1ec8-766f-a03d-a7ac4f0996b9
        ///
        /// </remarks>
        /// <param name="id">Идентификатор сопоставления.</param>
        /// <returns>Базовый формат данных сопоставления.</returns>
        [HttpGet("characteristic-comparisons/{id}")]
        public async Task<IActionResult> GetCharacteristicComparison(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.GetAsync($"/api/BulletinCharacteristicComparison/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Создание сопоставления характеристики с объявлением.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /api/BulletinGateway/characteristic-comparisons
        ///     {
        ///        "bulletinId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///        "characteristicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///        "characteristicValueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        ///
        /// </remarks>
        /// <param name="comparison">Формат данных создания сопоставления.</param>
        /// <returns>Базовый формат данных сопоставления.</returns>
        [HttpPost("characteristic-comparisons")]
        public async Task<IActionResult> CreateCharacteristicComparison([FromBody] JsonElement comparison)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PostAsJsonAsync("/api/BulletinCharacteristicComparison", comparison);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Изменение сопоставления характеристики с объявлением.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /api/BulletinGateway/characteristic-comparisons/01992a7c-71f5-7081-9311-44884bac8d58
        ///     {
        ///        "characteristicValueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Идентификатор сопоставления, которое нужно обновить.</param>
        /// <param name="comparison">Формат данных изменения сопоставления.</param>
        /// <returns>Базовый формат данных сопоставления.</returns>
        [HttpPut("characteristic-comparisons/{id}")]
        public async Task<IActionResult> UpdateCharacteristicComparison(Guid id, [FromBody] JsonElement comparison)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.PutAsJsonAsync($"/api/BulletinCharacteristicComparison/{id}", comparison);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        /// <summary>
        /// Удалить сопоставление характеристики с объявлением.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE /api/BulletinGateway/characteristic-comparisons/01992529-1ec8-766f-a03d-a7ac4f0996b9
        ///
        /// </remarks>
        /// <param name="id">Идентификатор сопоставления, которое нужно удалить.</param>
        /// <returns>true если все прошло успешно.</returns>
        [HttpDelete("characteristic-comparisons/{id}")]
        public async Task<IActionResult> DeleteCharacteristicComparison(Guid id)
        {
            var client = _httpClientFactory.CreateClient("BulletinService");
            var response = await client.DeleteAsync($"/api/BulletinCharacteristicComparison/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
    }
}