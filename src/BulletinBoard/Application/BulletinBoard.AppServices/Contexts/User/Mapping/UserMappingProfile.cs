using AutoMapper;
using BulletinBoard.Contracts.User.ApplicationUserDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using BulletinBoard.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Mapping;

/// <summary>
/// Настройки автомаппера
/// </summary>
public class UserMappingProfile : Profile
{
    /// <summary>
    /// Настройки автомаппера
    /// </summary>
    public UserMappingProfile()
    {
        CreateMap<ApplicationUserCreateDto, ApplicationUser>();
        CreateMap<ApplicationUser, ApplicationUserDto>();

    }
}
