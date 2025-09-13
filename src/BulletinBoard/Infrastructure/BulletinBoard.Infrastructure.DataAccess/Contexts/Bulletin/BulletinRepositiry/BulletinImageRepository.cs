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
        var imageData = await _context.BulletinImage
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (imageData == null) return null;

        return _mapper.Map<BulletinImageDto>(imageData);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinImageDto>> FindAsync(ExtendedSpecification<BulletinImage> specification)
    {
        var query = _dbSet.AsQueryable();

        query = query.ApplyExtendedSpecification(specification);

        return await query
            .Select(bulletinImage => _mapper.Map<BulletinImageDto>(bulletinImage))
            .ToListAsync();
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<BulletinImageDto> CreateAsync(BulletinImageCreateDto imageDto)
    {
        BulletinImage? imageData = _mapper.Map<BulletinImage>(imageDto);
        EntityEntry<BulletinImage> imageDataEntry = await _context.AddAsync(imageData);
        imageData = imageDataEntry.Entity;
        BulletinImageDto bulletinImageDto = _mapper.Map<BulletinImageDto>(imageData);

        return bulletinImageDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinImageDto?> UpdateAsync(Guid id, BulletinImageUpdateDto imageDto)
    {
        BulletinImage? imageData = await _context.BulletinImage
            .FirstOrDefaultAsync(i => i.Id == id);

        if (imageData == null) { return null; }

        _mapper.Map(imageDto, imageData);

        return _mapper.Map<BulletinImageDto>(imageData);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        BulletinImage? imageData = await _context.BulletinImage
            .FirstOrDefaultAsync(i => i.Id == id);

        if (imageData == null) return false;

        _context.Remove(imageData);

        return true;
    }
}
