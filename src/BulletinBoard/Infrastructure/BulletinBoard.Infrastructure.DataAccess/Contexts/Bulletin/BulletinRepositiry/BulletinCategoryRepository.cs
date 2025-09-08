using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry
{
    public class BulletinCategoryRepository : BulletinBaseRepository, IBulletinCategoryRepository
    {
        public BulletinCategoryRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<BulletinCategoryDto?> GetByIdAsync(Guid id)
        {
            var category = await _context.BulletinCategory
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id); 

            if (category == null) return null;

            return _mapper.Map<BulletinCategoryDto>(category);
        }

        public async Task<IReadOnlyCollection<BulletinCategoryDto>> FindAsync(Specification<BulletinCategory> predicate)
        {
            return await _context.BulletinCategory
                .Where(predicate.ToExpression())
                .Select(bulletinCategory => _mapper.Map<BulletinCategoryDto>(bulletinCategory))
                .ToListAsync();
        }

        public async Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto categoryDto)
        {
            BulletinCategory category = _mapper.Map<BulletinCategory>(categoryDto);
            var categoryEntry = await _context.AddAsync(category);
            category = categoryEntry.Entity;
            BulletinCategoryDto bulletinCategoryDto = _mapper.Map<BulletinCategoryDto>(category);

            return bulletinCategoryDto;
        }


        public async Task<BulletinCategoryDto?> UpdateAsync(Guid id, BulletinCategoryUpdateDto categoryDto)
        {
            var category = await _context.BulletinCategory
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) { return null; }

            category  = _mapper.Map<BulletinCategory>(categoryDto); 

            return _mapper.Map<BulletinCategoryDto>(category);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _context.BulletinCategory
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return false;

            _context.Remove(category);

            return true;
        }

        public async Task<bool> IsTheIdExist(Guid id)
        {
            return await _context.BulletinCategory.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> IsTheCategoryNameExist(string categoryName)
        {
            var bulletinCategoryCount = await _context.BulletinCategory
                .Where(c => c.CategoryName == categoryName)
                .CountAsync();

            return bulletinCategoryCount > 0;
        }

        public async Task<bool> IsTheCategoryLeafy(Guid id)
        {
            var category = await _context.BulletinCategory.FindAsync(id);
            return category?.IsLeafy ?? false;
        }
    }
}
