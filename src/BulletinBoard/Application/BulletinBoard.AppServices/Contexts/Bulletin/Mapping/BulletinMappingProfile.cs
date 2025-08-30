using AutoMapper;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Mapping
{
    public class BulletinMappingProfile : Profile
    {
        public BulletinMappingProfile()
        {
            CreateMap<BulletinCategoryCreateDto, BulletinCategory>();
            CreateMap<BulletinCategoryDto, BulletinCategory>();
            CreateMap<BulletinCategoryUpdateDto, BulletinCategory>();
            //Другие маппинги
        }
    }
}
