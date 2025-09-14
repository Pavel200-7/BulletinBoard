using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.Extensions;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinImageRepository : BulletinBaseRepository, IBulletinImageRepository
{
    protected readonly DbSet<BulletinImage> _dbSet;

    /// <inheritdoc/>
    public BulletinImageRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinImage>();
    }

    /// <inheritdoc/>
    public async Task<BulletinImageDto?> GetByIdAsync(Guid id)
    {
        BulletinImage? image = await _context.BulletinImage
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

        if (image == null) return null;

        return _mapper.Map<BulletinImageDto>(image);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinImageDto>> FindAsync(ExtendedSpecification<BulletinImage> specification)
    {
        IQueryable<BulletinImage> query = _dbSet.AsQueryable();

        query = query.ApplyExtendedSpecification(specification);

        return await query
            .Select(image => _mapper.Map<BulletinImageDto>(image))
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<BulletinImageDto> CreateAsync(BulletinImageCreateDto imageDto)
    {
        BulletinImage image = _mapper.Map<BulletinImage>(imageDto);
        EntityEntry<BulletinImage> imageEntry = await _context.AddAsync(image);
        image = imageEntry.Entity;
        return _mapper.Map<BulletinImageDto>(image);
    }

    /// <inheritdoc/>
    public async Task<BulletinImageDto?> UpdateAsync(Guid id, BulletinImageUpdateDto imageDto)
    {
        BulletinImage? image = await _context.BulletinImage
            .FirstOrDefaultAsync(i => i.Id == id);

        if (image == null) return null;

        _mapper.Map(imageDto, image);

        return _mapper.Map<BulletinImageDto>(image);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        BulletinImage? image = await _context.BulletinImage
            .FirstOrDefaultAsync(i => i.Id == id);

        if (image == null) return false;

        _context.Remove(image);
        return true;
    }
}