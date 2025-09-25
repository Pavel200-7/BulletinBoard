//using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
//using BulletinBoard.Contracts.Bulletin.BelletinMain;
//using BulletinBoard.Contracts.Errors;
//using Microsoft.AspNetCore.Mvc;

//namespace BulletinBoard.Hosts.Api.Controllers;

///// <summary>
///// Контроллер для работы с объявлениями.
///// </summary>
//[ApiController]
//[Route("api/[controller]")]
//[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
//public class BulletinMainController : ControllerBase
//{
//    private readonly IBulletinMainService _bulletinService;

//    /// <inheritdoc/>
//    public BulletinMainController(IBulletinMainService bulletinService)
//    {
//        _bulletinService = bulletinService;
//    }


//    /// <summary>
//    /// Создать объявление.
//    /// </summary>
//    /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     POST /BulletinMain
//    ///     {
//    ///        "bulletinUserId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
//    ///        "title": "Название объявления",
//    ///        "description": "Описание объявления",
//    ///        "categoryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
//    ///        "price": 1000
//    ///     }
//    ///
//    /// </remarks> 
//    /// <param name="createDto">Данные создания объявления.</param>
//    /// <param name="cancellationToken">Токен отмены.</param>
//    /// <returns>Данные объявления.</returns>
//    [HttpPost]
//    [ProducesResponseType(typeof(BulletinMainDto), StatusCodes.Status201Created)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    public async Task<IActionResult> Create(BulletinMainCreateDto createDto, CancellationToken cancellationToken)
//    {
//        var createdBulletin = await _bulletinService.CreateAsync(createDto, cancellationToken);
//        return Ok(createdBulletin);
//    }

//    /// <summary>
//    /// Обновление объявления по id.
//    /// </summary>
//    /// /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     PUT /BulletinMain/3fa85f64-5717-4562-b3fc-2c963f66afa6
//    ///     {
//    ///        "title": "Название объявления",
//    ///        "description": "Описание объявления",
//    ///        "price": 1000
//    ///     }
//    ///
//    /// </remarks> 
//    /// <param name="id">Id объявления.</param>
//    /// <param name="cancellationToken">Токен отмены.</param>
//    /// <param name="updateDto">Данные обновления объявления.</param>
//    /// <returns>Данные объявления.</returns>
//    [HttpPut("{id}")]
//    [ProducesResponseType(typeof(BulletinMainDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> Update(Guid id, BulletinMainUpdateDto updateDto, CancellationToken cancellationToken)
//    {
//        var updatedBulletin = await _bulletinService.UpdateAsync(id, updateDto, cancellationToken);
//        return Ok(updatedBulletin);
//    }

//    /// <summary>
//    /// Удаление объявления по id.
//    /// </summary>
//    /// <remarks>
//    /// Пример запроса:
//    ///
//    ///     Delete /BulletinMain/01992529-1ec8-766f-a03d-a7ac4f0996b9
//    ///
//    /// </remarks>
//    /// <param name="id">Идентификатор объявеления, которое нужно удалить.</param>
//    /// <param name="cancellationToken">Токен отмены.</param>
//    /// <returns>true если все прошло успешно.</returns>
//    [HttpDelete("{id}")]
//    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
//    {
//        bool result = await _bulletinService.DeleteAsync(id, cancellationToken);
//        return Ok(result);
//    }


//}