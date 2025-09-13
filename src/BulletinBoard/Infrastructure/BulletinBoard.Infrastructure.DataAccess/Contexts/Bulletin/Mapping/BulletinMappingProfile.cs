using AutoMapper;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.Mapping
{
    public class BulletinMappingProfile : Profile
    {
        public BulletinMappingProfile()
        {
            // BulletinCategory
            CreateMap<BulletinCategoryCreateDto, BulletinCategory>().ReverseMap();
            CreateMap<BulletinCategoryDto, BulletinCategory>().ReverseMap();
            CreateMap<BulletinCategoryUpdateDto, BulletinCategory>().ReverseMap();

            // BulletinImage
            CreateMap<BulletinImageCreateDto, BulletinImage>().ReverseMap();
            CreateMap<BulletinImageDto, BulletinImage>().ReverseMap();
            CreateMap<BulletinImageUpdateDto, BulletinImage>().ReverseMap();

            //Другие маппинги
        }
    }
}
