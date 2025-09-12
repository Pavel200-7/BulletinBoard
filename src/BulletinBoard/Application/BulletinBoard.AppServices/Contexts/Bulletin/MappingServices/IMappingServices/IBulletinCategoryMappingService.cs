using BulletinBoard.Contracts.Bulletin.BulletinCategory;


namespace BulletinBoard.AppServices.Contexts.Bulletin.MappingServices.IMappingServices;

public interface IBulletinCategoryMappingService
{
    public Task<BulletinCategoryReadAllDto> ConvertToBulletinCategoryReadAllDto(IReadOnlyCollection<BulletinCategoryDto> categoriesDtoCollection);

    public Task<BulletinCategoryReadSingleDto> ConvertToBulletinCategoryReadSingleDto(IReadOnlyCollection<BulletinCategoryDto> categoriesDtoCollection);
}

