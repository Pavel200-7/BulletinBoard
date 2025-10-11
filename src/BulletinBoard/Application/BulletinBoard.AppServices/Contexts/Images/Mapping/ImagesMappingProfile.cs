using AutoMapper;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.Images.Image.ReadDto;
using BulletinBoard.Contracts.Images.Новая_папка;
using BulletinBoard.Domain.Entities.Images;


namespace BulletinBoard.AppServices.Contexts.Images.Mapping;

/// <summary>
/// Настройки автомаппера
/// </summary>
public class ImagesMappingProfile : Profile
{
    /// <summary>
    /// Настройки автомаппера
    /// </summary>
    public ImagesMappingProfile()
    {
        CreateMap<ImageCreateDto, ImageMetadata>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ImageMetadata, ImageInfoReadDto>();
    }
}