using AutoMapper;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.Mapping;

/// <summary>
/// Настройки автомаппера
/// </summary>
public class BulletinMappingProfile : Profile
{
    /// <summary>
    /// Настройки автомаппера
    /// </summary>
    public BulletinMappingProfile()
    {
        // BulletinCategory
        CreateMap<BulletinCategoryCreateDto, BulletinCategory>().ReverseMap();
        CreateMap<BulletinCategoryDto, BulletinCategory>().ReverseMap();
        CreateMap<BulletinCategoryUpdateDto, BulletinCategory>().ReverseMap();

     

        //Другие маппинги
    }
}
