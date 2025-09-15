using BulletinBoard.Contracts.Bulletin.BulletinCategory;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Mapping.MappingServices.IMappingServices;

/// <summary>
/// Класс для преобразования ДТО BulletinCategory
/// </summary>
public interface IBulletinCategoryMappingService
{
    /// <summary>
    /// Преобразовать базовые коллекцию базовых ДТО в иерархическую структуру.
    /// </summary>
    /// <param name="categoriesDtoCollection">Коллекция категорий в базовом формате.</param>
    /// <returns>Формат данных для вывода всех категорий в их правильном иерархическом виде.</returns>
    public Task<BulletinCategoryReadAllDto> ConvertToBulletinCategoryReadAllDto(IReadOnlyCollection<BulletinCategoryDto> categoriesDtoCollection);

    /// <summary>
    /// Преобразовать базовые коллекцию базовых ДТО в одиночую иерархическую структуру.
    /// </summary>
    /// <param name="categoriesDtoCollection">Коллекция категорий в базовом формате.</param>
    /// <returns>Формат данных для вывода одной карегории в виде древовидной струкруры от самого корня</returns>
    public Task<BulletinCategoryReadSingleDto> ConvertToBulletinCategoryReadSingleDto(IReadOnlyCollection<BulletinCategoryDto> categoriesDtoCollection);
}

