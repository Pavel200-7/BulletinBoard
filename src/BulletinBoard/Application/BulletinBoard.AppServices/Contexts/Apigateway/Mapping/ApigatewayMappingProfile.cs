using AutoMapper;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Apigateway.Mapping;

public class ApigatewayMappingProfile : Profile
{
    public ApigatewayMappingProfile()
    {
        CreateMap<ApplicationUserCreateDto, BulletinUserCreateDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
        

    }

}
