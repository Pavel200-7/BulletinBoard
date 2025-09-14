using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.Extensions;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinCategoryRepository : BulletinBaseRepository, IBulletinCategoryRepository
{
    protected readonly DbSet<BulletinCategory> _dbSet;

    /// <inheritdoc/>
    public BulletinCategoryRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinCategory>();
    }

    /// <inheritdoc/>
    public async Task<BulletinCategoryDto?> GetByIdAsync(Guid id)
    {
        BulletinCategory? category = await _context.BulletinCategory
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null) return null;

        return _mapper.Map<BulletinCategoryDto>(category);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinCategoryDto>> FindAsync(ExtendedSpecification<BulletinCategory> specification)
    {
        IQueryable<BulletinCategory> query = _dbSet.AsQueryable();

        query = query.ApplyExtendedSpecification(specification);

        return await query
            .Select(bulletinCategory => _mapper.Map<BulletinCategoryDto>(bulletinCategory))
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto categoryDto)
    {
        BulletinCategory category = _mapper.Map<BulletinCategory>(categoryDto);
        EntityEntry<BulletinCategory> categoryEntry = await _context.AddAsync(category);
        category = categoryEntry.Entity;
        BulletinCategoryDto bulletinCategoryDto = _mapper.Map<BulletinCategoryDto>(category);

        return bulletinCategoryDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinCategoryDto?> UpdateAsync(Guid id, BulletinCategoryUpdateDto categoryDto)
    {
        BulletinCategory? category = await _context.BulletinCategory
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null) { return null; }

        _mapper.Map(categoryDto, category);

        return _mapper.Map<BulletinCategoryDto>(category);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        BulletinCategory? category = await _context.BulletinCategory
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null) return false;

        _context.Remove(category);

        return true;
    }
}
