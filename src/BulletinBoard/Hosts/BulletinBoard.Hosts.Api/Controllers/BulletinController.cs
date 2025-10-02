using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.UpdateDto;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с объявлениями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class BulletinController : ControllerBase
{
    private readonly IBulletinMainService _bulletinMainService;
    private readonly IBulletinService _bulletinService;



    /// <inheritdoc/>
    public BulletinController
        (
        IBulletinMainService bulletinMainService,
        IBulletinService bulletinService
        )
    {
        _bulletinMainService = bulletinMainService;
        _bulletinService = bulletinService;
    }

    /// <summary>
    /// Получить значение объявление по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET /Bulletin/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор объявления.</param>
    /// <returns>Данные объявления..</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BulletinDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dto = await _bulletinService.GetByIdAsync(id);
        return Ok(dto);
    }

    /// <summary>
    /// Получить значение объявление по id.
    /// (Без избыточных идентификаторов)
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET /Bulletin/01992529-1ec8-766f-a03d-a7ac4f0996b9/Single
    ///
    /// </remarks>
    /// <param name="id">Идентификатор объявления.</param>
    /// <returns>Данные объявления..</returns>
    [HttpGet("{id}/Single")]
    [ProducesResponseType(typeof(BulletinReadSingleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdReadSingle(Guid id)
    {
        var dto = await _bulletinService.GetByIdReadSingleAsync(id);
        return Ok(dto);
    }

    /// <summary>
    /// Получить отсортированную и отфильтрованную выборку объявлений (страницу).
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    POST /Bulletin/Bulletins 
    ///    {
    ///         "limit": 0, (от 5 до 50)
    ///         "sortBy": "Date", ()
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
    [HttpPost("Bulletins")]
    [ProducesResponseType(typeof(BulletinReadPagenatedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBulletins(BulletinPaginationRequestDto request)
    {
        var dto = await _bulletinService.GetBulletinsAsync(request);
        return Ok(new BulletinReadPagenatedDto());
    }

    /// <summary>
    /// Создать объявление.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    Post /Bulletin
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
    ///             {
    ///                 "characteristicId": "3fa85f64-5717-4562-b3fc-2c963f66afa1",
    ///                 "characteristicValueId": "3fa85f64-5717-4562-b3fc-2c963f66afa0"
    ///             }
    ///             {
    ///                 "characteristicId": "3fa85f64-5717-4562-b3fc-2c963f66afa5",
    ///                 "characteristicValueId": "3fa85f64-5717-4562-b3fc-2c963f211afa6"
    ///             }
    ///         ]
    ///     }
    ///
    /// </remarks>
    /// <param name="bulletinCreateDtoAPIVersion">Формат данных создания нового объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Id объявления.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBulletin(BulletinCreateDtoRequest bulletinCreateDtoAPIVersion, CancellationToken cancellationToken)
    {
        List<BulletinImageCreateDtoWhileBulletinCreating> imagesCreateDto = new()
        {
            new BulletinImageCreateDtoWhileBulletinCreating() { Id = Guid.NewGuid(), IsMain = true },
            new BulletinImageCreateDtoWhileBulletinCreating() { Id = Guid.NewGuid(), IsMain = false },
            new BulletinImageCreateDtoWhileBulletinCreating() { Id = Guid.NewGuid(), IsMain = false },
            new BulletinImageCreateDtoWhileBulletinCreating() { Id = Guid.NewGuid(), IsMain = false },
            new BulletinImageCreateDtoWhileBulletinCreating() { Id = Guid.NewGuid(), IsMain = false },
        };

        BulletinCreateDtoController bulletinCreateDto = new BulletinCreateDtoController
        {
            BulletinMain = bulletinCreateDtoAPIVersion.BulletinMain,
            CharacteristicComparisons = bulletinCreateDtoAPIVersion.CharacteristicComparisons,
            Images = imagesCreateDto
        };

        Guid bulletinId = await _bulletinService.CreateAsync(bulletinCreateDto, cancellationToken);
        return Ok(bulletinId);
    }


    /// <summary>
    /// Обновить объявление.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    Put /Bulletin
    ///    {
    ///         "title": "Заголовок объявления.",
    ///         "description": "Описание объявления.",
    ///         "price": 1000
    ///     }
    ///
    /// </remarks>
    /// <param name="id">Id объявления.</param>
    /// <param name="updateDto">Данные обновления объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Id объявления.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBulletin(Guid id, BulletinMainUpdateDto updateDto, CancellationToken cancellationToken)
    {
        var bulletinDto = await _bulletinMainService.UpdateAsync(id, updateDto, cancellationToken);
        Guid bulletinId = bulletinDto.Id;
        return Ok(bulletinId);
    }

    /// <summary>
    /// Удалить объявление.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    Delete /Bulletin
    ///
    /// </remarks>
    /// <param name="id">Id объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат удаления.</returns>
    [HttpDelete]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBulletin(Guid id, CancellationToken cancellationToken)
    {
        bool result = await _bulletinMainService.DeleteAsync(id, cancellationToken);
        return Ok(result);
    }
}
