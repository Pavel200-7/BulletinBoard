using AutoMapper;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.Mapping
{
    public class BulletinMappingProfile : Profile
    {
        public BulletinMappingProfile()
        {
            // Не используется
            CreateMap<BulletinCategoryCreateDto, BulletinCategory>().ReverseMap();
            CreateMap<BulletinCategoryDto, BulletinCategory>().ReverseMap();
            CreateMap<BulletinCategoryUpdateDto, BulletinCategory>().ReverseMap();
            //Другие маппинги
        }
    }
}
