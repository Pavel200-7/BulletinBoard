using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers;


/// <summary>
/// Контроллер для работы с категориями объявлений.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class BulletinCategoryController : ControllerBase
{
    private readonly IBulletinCategoryService _bulletinCategoryService;

    /// <inheritdoc/>
    public BulletinCategoryController(IBulletinCategoryService bulletinCategoryService)
    {
        _bulletinCategoryService = bulletinCategoryService;
    }

    /// <summary>
    /// Получение категории по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET /BulletinCategory/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор категории.</param>
    /// <returns>Базовый формат данных категории.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BulletinCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBulletinCategory(Guid id)
    {
        var categoryDto = await _bulletinCategoryService.GetByIdAsync(id);
        return Ok(categoryDto);
    }

    /// <summary>
    /// Создание категории.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /BulletinCategory
    ///     {
    ///        "parentCategoryId" : "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "categoryName" : "Категория 1",
    ///        "isLeafy": false
    ///     }
    ///     
    ///     POST /BulletinCategory
    ///     {
    ///        "parentCategoryId" : null,
    ///        "categoryName" : "Категория 2",
    ///        "isLeafy": true
    ///     }
    ///
    /// </remarks>
    /// <param name="category">Формат данных создания категории.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных категории.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BulletinCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBulletinCategory(BulletinCategoryCreateDto category, CancellationToken cancellationToken)
    {
        var createdCategory = await _bulletinCategoryService.CreateAsync(category, cancellationToken);
        return Ok(createdCategory);
    }

    /// <summary>
    /// Изменение категории.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     PUT /BulletinCategory/01992a7c-71f5-7081-9311-44884bac8d58
    ///     {
    ///        "parentCategoryId" : "0199252f-2d8c-7892-ada3-91600c780739",
    ///        "categoryName" : "Категория 5",
    ///     }
    ///     
    ///     PUT /BulletinCategory/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///     {
    ///        "parentCategoryId" : null,
    ///        "categoryName" : "Категория 2",
    ///     }
    ///
    /// </remarks>
    /// <param name="id">Идентификатор категории, которую нужно обновить.</param>
    /// <param name="category">Формат данных изменения категории.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных категории.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BulletinCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBulletinCategory(Guid id, BulletinCategoryUpdateDto category, CancellationToken cancellationToken)
    {
        var updetedCategory = await _bulletinCategoryService.UpdateAsync(id, category, cancellationToken);
        return Ok(updetedCategory);
    }

    /// <summary>
    /// Удалить категории.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     Delete /BulletinCategory/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор категории, которую нужно удалить.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>true если все прошло успешно.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBulletinCategory(Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _bulletinCategoryService.DeleteAsync(id, cancellationToken);
        return Ok(isDeleted);
    }

    /// <summary>
    /// Получение всех категорий в их правильном иерархическом виде.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     Get /BulletinCategory
    ///
    /// </remarks>
    /// <returns>Формат данных для вывода всех категорий в их правильном иерархическом виде.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(BulletinCategoryReadAllDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBulletinCategory()
    {
        var categoryDto = await _bulletinCategoryService.GetAllAsync();
        return Ok(categoryDto);
    }


    /// <summary>
    /// Получение одной карегории в виде древовидной струкруры от самого корня.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     Get /BulletinCategory/01992529-1ec8-766f-a03d-a7ac4f0996b9/SingleChain
    ///
    /// </remarks>
    /// <returns>Формат данных для вывода одной карегории в виде древовидной струкруры от самого корня.</returns>
    [HttpGet("{id}/SingleChain")]
    [ProducesResponseType(typeof(BulletinCategoryReadAllDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetsingleBulletinCategory(Guid id)
    {
        var categoryDto = await _bulletinCategoryService.GetSingleAsync(id);
        return Ok(categoryDto);
    }
}
