using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.FilterDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с категориями объявлений.
/// </summary>
public interface IBulletinCategoryService : IBaseCRUDService
    <
    BulletinCategoryDto,
    BulletinCategoryCreateDto,
    BulletinCategoryUpdateDto
    >
{
    /// <summary>
    /// Получить список категорий по фильтру.
    /// </summary>
    /// <param name="categoryFilterDto">Формат данных для фильтрации категории по id родительской и (или) по названию.</param>
    /// <returns>Коллекция базовый формат данных категории.</returns>
    public Task<IReadOnlyCollection<BulletinCategoryDto>> GetAsync(BulletinCategoryFilterDto categoryFilterDto);

    /// <summary>
    /// Получить категории в формате правильной иерархии.
    /// </summary>
    /// <returns>Формат данных для вывода всех категорий в их правильном иерархическом виде.</returns>
    public Task<BulletinCategoryReadAllDto> GetAllAsync();

    /// <summary>
    /// Получить карегорию в виде древовидной струкруры от самого корня.
    /// </summary>
    /// <param name="id">Id категории.</param>
    /// <returns> Формат данных для вывода одной карегории в виде древовидной струкруры от самого корня.</returns>
    public Task<BulletinCategoryReadSingleDto> GetSingleAsync(Guid id);
}
