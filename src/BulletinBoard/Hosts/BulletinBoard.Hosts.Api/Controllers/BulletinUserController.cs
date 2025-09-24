//using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
//using BulletinBoard.Contracts.Bulletin.BulletinUser;
//using BulletinBoard.Contracts.Errors;
//using Microsoft.AspNetCore.Mvc;

//namespace BulletinBoard.Hosts.Api.Controllers;

///// <summary>
///// Контроллер для работы с сущностью пользователей объявлений.
///// </summary>
//[ApiController]
//[Route("api/[controller]")]
//[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
//public class BulletinUserController : ControllerBase
//{
//    private readonly IBulletinUserService _bulletinUserService;

//    /// <inheritdoc/>
//    public BulletinUserController(IBulletinUserService bulletinUserService)
//    {
//        _bulletinUserService = bulletinUserService;
//    }

//    /// <summary>
//    /// Получение пользователя по идентификатору.
//    /// </summary>
//    /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     GET /BulletinUser/01992529-1ec8-766f-a03d-a7ac4f0996b9
//    ///
//    /// </remarks>
//    /// <param name="id">Идентификатор пользователя.</param>
//    /// <returns>Данные пользователя.</returns>
//    [HttpGet("{id}")]
//    [ProducesResponseType(typeof(BulletinUserDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> GetUser(Guid id)
//    {
//        var userDto = await _bulletinUserService.GetByIdAsync(id);
//        return Ok(userDto);
//    }

//    /// <summary>
//    /// Поиск пользователей по фильтру.
//    /// </summary>
//    /// <remarks>
//    /// Примеры запросов:
//    ///
//    ///     POST /BulletinUser/search
//    ///     {
//    ///         "isUsedFullName": true,
//    ///         "isUsedFullNameContains": true,
//    ///         "fullName": "Ивен Иванов",
//    ///         "isUsedBlocked": false,
//    ///         "blocked": false,
//    ///         "isUsedCoordinates": true,
//    ///         "latitude": 44,37,
//    ///         "longitude": 33,31,
//    ///         "isUsedCoordinatesEquals": false,
//    ///         "isUsedCoordinatesCloser": true,
//    ///         "isUsedCoordinatesFarther": false,
//    ///         "distance": 5,
//    ///         "isUsedFormattedAddress": true,
//    ///         "formattedAddress": "Севастополь...",
//    ///         "isUsedPhone": true,
//    ///         "phone": "+00000000000"
//    ///     }
//    ///
//    /// </remarks>
//    /// <param name="filter">Параметры фильтрации пользователей.</param>
//    /// <returns>Коллекция пользователей, удовлетворяющих фильтру.</returns>
//    [HttpPost("search")]
//    [ProducesResponseType(typeof(IReadOnlyCollection<BulletinUserDto>), StatusCodes.Status200OK)]
//    public async Task<IActionResult> SearchUsers([FromBody] BulletinUserFilterDto filter)
//    {
//        var users = await _bulletinUserService.GetAsync(filter);
//        return Ok(users);
//    }

//    /// <summary>
//    /// Создание нового пользователя.
//    /// </summary>
//    /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     POST /BulletinUser
//    ///     {
//    ///         "fullName": "Иван Иванов",
//    ///         "latitude": 55.7558,
//    ///         "longitude": 37.6176,
//    ///         "formattedAddress": "Москва...",
//    ///         "phone": "+79161234567"
//    ///     }
//    ///
//    /// </remarks>
//    /// <param name="userDto">Данные для создания пользователя.</param>
//    /// <returns>Созданный пользователь.</returns>
//    [HttpPost]
//    [ProducesResponseType(typeof(BulletinUserDto), StatusCodes.Status201Created)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    public async Task<IActionResult> CreateUser([FromBody] BulletinUserCreateDto userDto)
//    {
//        var createdUser = await _bulletinUserService.CreateAsync(userDto);
//        return Ok(createdUser);
//    }

//    /// <summary>
//    /// Изменение имени пользователя.
//    /// </summary>
//    /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     PATCH /BulletinUser/01992529-1ec8-766f-a03d-a7ac4f0996b9/name
//    ///     "Петр Петров"
//    ///
//    /// </remarks>
//    /// <param name="id">Идентификатор пользователя.</param>
//    /// <param name="name">Новое имя пользователя.</param>
//    /// <returns>Обновленный пользователь.</returns>
//    [HttpPatch("{id}/name")]
//    [ProducesResponseType(typeof(BulletinUserDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> ChangeUserName(Guid id, [FromBody] string name)
//    {
//        var updetedUser = await _bulletinUserService.ChangeNameAsync(id, name);
//        return Ok(updetedUser);
//    }

//    /// <summary>
//    /// Изменение местоположения пользователя.
//    /// </summary>
//    /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     PATCH /BulletinUser/01992529-1ec8-766f-a03d-a7ac4f0996b9/location
//    ///     {
//    ///         "latitude": 59.9343,
//    ///         "longitude": 30.3351,
//    ///         "formattedAddress": "Санкт-Петербург, Дворцовая площадь"
//    ///     }
//    ///
//    /// </remarks>
//    /// <param name="id">Идентификатор пользователя.</param>
//    /// <param name="locationDto">Новые данные местоположения.</param>
//    /// <returns>Обновленный пользователь.</returns>
//    [HttpPatch("{id}/location")]
//    [ProducesResponseType(typeof(BulletinUserDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> ChangeUserLocation(Guid id, [FromBody] BulletinUserUpdateLocationDto locationDto)
//    {
//        var userDto = await _bulletinUserService.ChangeAdressAsync(id, locationDto);
//        return Ok(userDto);
//    }

//    /// <summary>
//    /// Изменение телефона пользователя.
//    /// </summary>
//    /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     PATCH /BulletinUser/01992529-1ec8-766f-a03d-a7ac4f0996b9/phone
//    ///     "+79167654321"
//    ///
//    /// </remarks>
//    /// <param name="id">Идентификатор пользователя.</param>
//    /// <param name="phone">Новый номер телефона.</param>
//    /// <returns>Обновленный пользователь.</returns>
//    [HttpPatch("{id}/phone")]
//    [ProducesResponseType(typeof(BulletinUserDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> ChangeUserPhone(Guid id, [FromBody] string phone)
//    {
//        var userDto = await _bulletinUserService.ChangePhoneAsync(id, phone);
//        return Ok(userDto);
//    }

//    /// <summary>
//    /// Блокировка пользователя.
//    /// </summary>
//    /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     POST /BulletinUser/01992529-1ec8-766f-a03d-a7ac4f0996b9/block
//    ///
//    /// </remarks>
//    /// <param name="id">Идентификатор пользователя.</param>
//    /// <returns>Заблокированный пользователь.</returns>
//    [HttpPost("{id}/block")]
//    [ProducesResponseType(typeof(BulletinUserDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> BlockUser(Guid id)
//    {
//        var userDto = await _bulletinUserService.BlockUserAsync(id);
//        return Ok(userDto);
//    }

//    /// <summary>
//    /// Разблокировка пользователя.
//    /// </summary>
//    /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     POST /BulletinUser/01992529-1ec8-766f-a03d-a7ac4f0996b9/unblock
//    ///
//    /// </remarks>
//    /// <param name="id">Идентификатор пользователя.</param>
//    /// <returns>Разблокированный пользователь.</returns>
//    [HttpPost("{id}/unblock")]
//    [ProducesResponseType(typeof(BulletinUserDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> UnblockUser(Guid id)
//    {
//        var userDto = await _bulletinUserService.UnBlockUserAsync(id);
//        return Ok(userDto);
//    }

//    /// <summary>
//    /// Удаление пользователя.
//    /// </summary>
//    /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     DELETE /BulletinUser/01992529-1ec8-766f-a03d-a7ac4f0996b9
//    ///
//    /// </remarks>
//    /// <param name="id">Идентификатор пользователя.</param>
//    /// <returns>Результат удаления.</returns>
//    [HttpDelete("{id}")]
//    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> DeleteUser(Guid id)
//    {
//        var result = await _bulletinUserService.DeleteAsync(id);
//        return Ok(result);
//    }
//}