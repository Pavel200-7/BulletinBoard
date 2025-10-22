using AutoMapper;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;
using BulletinBoard.Contracts.Bulletin.BelletinMain.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.UpdateDto;
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
        CreateMap<BulletinCategoryUpdateDto, BulletinCategoryUpdateDtoForValidating>().ReverseMap();


        // BulletinCharacteristic
        CreateMap<BulletinCharacteristicCreateDto, BulletinCharacteristic>().ReverseMap();
        CreateMap<BulletinCharacteristicDto, BulletinCharacteristic>().ReverseMap();
        CreateMap<BulletinCharacteristicUpdateDto, BulletinCharacteristic>().ReverseMap();
        CreateMap<BulletinCharacteristicUpdateDto, BulletinCharacteristicUpdateDtoForValidating>().ReverseMap();
        CreateMap<BulletinCharacteristicDto, BulletinCharacteristicUpdateDtoForValidating>().ReverseMap();


        // BulletinCharacteristicComparison
        CreateMap<BulletinCharacteristicComparisonCreateDto, BulletinCharacteristicComparison>().ReverseMap();
        CreateMap<BulletinCharacteristicComparisonDto, BulletinCharacteristicComparison>().ReverseMap();
        CreateMap<BulletinCharacteristicComparisonUpdateDto, BulletinCharacteristicComparison>().ReverseMap();
        CreateMap<BulletinCharacteristicComparisonCreateDto, BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>().ReverseMap();
        CreateMap<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating, BulletinCharacteristicComparison>().ReverseMap();
        CreateMap<BulletinCharacteristicComparisonDto, BulletinCharacteristicComparisonUpdateDtoForValidating>().ReverseMap();
        CreateMap<BulletinCharacteristicComparisonUpdateDto, BulletinCharacteristicComparisonUpdateDtoForValidating>().ReverseMap();
        CreateMap<BulletinCharacteristicComparison, BulletinCharacteristicComparisonReadValueDto>().ReverseMap();
        CreateMap<BulletinCharacteristicComparison, BulletinCharacteristicComparisonBulletinReadDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Id))
            .ForMember(dest => dest.Characteristic, opt => opt.MapFrom(c => c.Characteristic.Name))
            .ForMember(dest => dest.CharacteristicValue, opt => opt.MapFrom(c => c.CharacteristicValue.Value));

        // BulletinCharacteristicValue
        CreateMap<BulletinCharacteristicValueCreateDto, BulletinCharacteristicValue>().ReverseMap();
        CreateMap<BulletinCharacteristicValueDto, BulletinCharacteristicValue>().ReverseMap();
        CreateMap<BulletinCharacteristicValueUpdateDto, BulletinCharacteristicValue>().ReverseMap();
        CreateMap<BulletinCharacteristicValueUpdateDto, BulletinCharacteristicValueUpdateDtoForValidating>().ReverseMap();
        CreateMap<BulletinCharacteristicValueDto, BulletinCharacteristicValueUpdateDtoForValidating>().ReverseMap();


        // BulletinImage
        CreateMap<BulletinImageCreateDto, BulletinImage>().ReverseMap();
        CreateMap<BulletinImageDto, BulletinImage>().ReverseMap();
        CreateMap<BulletinImageDto, BulletinImageUpdateDto>().ReverseMap();
        CreateMap<BulletinImageUpdateDto, BulletinImage>().ReverseMap();
        CreateMap<BulletinImageCreateDto, BulletinImageCreateDtoWhileBulletinCreating>().ReverseMap();
        CreateMap<BulletinImageCreateDtoWhileBulletinCreating, BulletinImage>().ReverseMap();
        CreateMap<BulletinImage, BulletinImageBulletinReadDto>();



        // BulletinMain
        CreateMap<BulletinMainCreateDto, BulletinMain>().ReverseMap();
        CreateMap<BulletinMainDto, BulletinMain>().ReverseMap();
        CreateMap<BulletinMainUpdateDto, BulletinMain>().ReverseMap();
        CreateMap<BulletinMainUpdateDto, BulletinMainUpdateDtoForValidating>();
        CreateMap<BulletinMainDto, BulletinMainUpdateDtoForValidating>();
        CreateMap<BulletinMainDto, BulletinMainBulletinReadDto>();
        CreateMap<BulletinMain, BulletinMainBulletinReadDto>();
        CreateMap<BulletinMainDto, BulletinMainUpdateDto>().ReverseMap();



        // BulletinRating
        CreateMap<BulletinRatingCreateDto, BulletinRating>().ReverseMap();
        CreateMap<BulletinRatingDto, BulletinRating>().ReverseMap();
        CreateMap<BulletinRatingUpdateDto, BulletinRating>().ReverseMap();
        CreateMap<BulletinRating, BulletinRatingBulletinReadDto>();
        

        // BulletinUser
        CreateMap<BulletinUserCreateDto, BulletinUser>().ReverseMap();
        CreateMap<BulletinUserDto, BulletinUser>().ReverseMap();
        CreateMap<BulletinUserUpdateDto, BulletinUser>().ReverseMap();
        CreateMap<BulletinUserCreateDto, BulletinUserDto>().ReverseMap();
        CreateMap<BulletinUserUpdateDto, BulletinUserDto>().ReverseMap();


        // BulletinViewsCount
        CreateMap<BulletinViewsCountCreateDto, BulletinViewsCount>().ReverseMap();
        CreateMap<BulletinViewsCountDto, BulletinViewsCount>().ReverseMap();
        CreateMap<BulletinViewsCountUpdateDto, BulletinViewsCount>().ReverseMap();
        CreateMap<BulletinViewsCountCreateDtoWhileBulletinCreating, BulletinViewsCount>().ReverseMap();
        CreateMap<BulletinViewsCount, BulletinViewsCountBulletinReadDto>();



        // Bulletin
        CreateMap<BulletinMain, BulletinDto>()
            .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src)) // Основная сущность
            .ForMember(dest => dest.CharacteristicComparisons, opt => opt.MapFrom(src => src.Characteristics))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.ViewsCount, opt => opt.MapFrom(src => src.ViewsCount));

        CreateMap<BulletinMain, BulletinReadSingleDto>()
           .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src)) // Основная сущность
           .ForMember(dest => dest.CharacteristicComparisons, opt => opt.MapFrom(src => src.Characteristics))
           .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings))
           .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
           .ForMember(dest => dest.ViewsCount, opt => opt.MapFrom(src => src.ViewsCount));


        CreateMap<BulletinMain, BulletinReadPagenatedItemDto>()
           .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src)) 
           .ForMember(dest => dest.ViewsCount, opt => opt.MapFrom(src => src.ViewsCount.ViewsCount))
           .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => 
                src.Ratings.Any()
                ? (decimal)src.Ratings.Sum(r => r.Rating) / src.Ratings.Count()
                : 0
                ))
            .ForMember(dest => dest.MainImageId, opt => opt.MapFrom(src =>
                src.Images
                .Where(i => i.IsMain == true)
                .Select(i => (Guid?)i.Id)
                .FirstOrDefault()
                )
            );
        
        //Другие маппинги
    }
}
