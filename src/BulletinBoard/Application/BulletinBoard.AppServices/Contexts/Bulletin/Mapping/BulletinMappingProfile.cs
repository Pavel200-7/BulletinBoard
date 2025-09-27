using AutoMapper;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BelletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BelletinMain.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;
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

        // BulletinCharacteristic
        CreateMap<BulletinCharacteristicCreateDto, BulletinCharacteristic>().ReverseMap();
        CreateMap<BulletinCharacteristicDto, BulletinCharacteristic>().ReverseMap();
        CreateMap<BulletinCharacteristicUpdateDto, BulletinCharacteristic>().ReverseMap();

        // BulletinCharacteristicComparison
        CreateMap<BulletinCharacteristicComparisonCreateDto, BulletinCharacteristicComparison>().ReverseMap();
        CreateMap<BulletinCharacteristicComparisonDto, BulletinCharacteristicComparison>().ReverseMap();
        CreateMap<BulletinCharacteristicComparisonUpdateDto, BulletinCharacteristicComparison>().ReverseMap();
        CreateMap<BulletinCharacteristicComparisonCreateDto, BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>().ReverseMap();
        CreateMap<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating, BulletinCharacteristicComparison>().ReverseMap();


        // BulletinCharacteristicValue
        CreateMap<BulletinCharacteristicValueCreateDto, BulletinCharacteristicValue>().ReverseMap();
        CreateMap<BulletinCharacteristicValueDto, BulletinCharacteristicValue>().ReverseMap();
        CreateMap<BulletinCharacteristicValueUpdateDto, BulletinCharacteristicValue>().ReverseMap();

        // BulletinImage
        CreateMap<BulletinImageCreateDto, BulletinImage>().ReverseMap();
        CreateMap<BulletinImageDto, BulletinImage>().ReverseMap();
        CreateMap<BulletinImageUpdateDto, BulletinImage>().ReverseMap();
        CreateMap<BulletinImageCreateDto, BulletinImageCreateDtoWhileBulletinCreating>().ReverseMap();
        CreateMap<BulletinImageCreateDtoWhileBulletinCreating, BulletinImage>().ReverseMap();



        // BulletinMain
        CreateMap<BulletinMainCreateDto, BulletinMain>().ReverseMap();
        CreateMap<BulletinMainDto, BulletinMain>().ReverseMap();
        CreateMap<BulletinMainUpdateDto, BulletinMain>().ReverseMap();

        // BulletinRating
        CreateMap<BulletinRatingCreateDto, BulletinRating>().ReverseMap();
        CreateMap<BulletinRatingDto, BulletinRating>().ReverseMap();
        CreateMap<BulletinRatingUpdateDto, BulletinRating>().ReverseMap();

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




        //Другие маппинги
    }
}
