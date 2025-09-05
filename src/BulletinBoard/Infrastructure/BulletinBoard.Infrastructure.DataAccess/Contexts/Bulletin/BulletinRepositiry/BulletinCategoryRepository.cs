using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry
{
    public class BulletinCategoryRepository : BulletinBaseRepository, IBulletinCategoryRepository
    {
        public BulletinCategoryRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto categoryDto)
        {
            BulletinCategory category = _mapper.Map<BulletinCategory>(categoryDto);
            var categoryEntry = _context.Add(category);
            category = categoryEntry.Entity;
            BulletinCategoryDto bulletinCategoryDto = _mapper.Map<BulletinCategoryDto>(category);

            return Task.FromResult<BulletinCategoryDto>(bulletinCategoryDto);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BulletinCategoryDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BulletinCategoryDto> UpdateAsync(Guid id, BulletinCategoryUpdateDto category)
        {
            throw new NotImplementedException();
        }
    }
}
