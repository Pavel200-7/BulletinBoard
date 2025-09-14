using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.Extensions;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinRatingRepository : BulletinBaseRepository, IBulletinRatingRepository
{
    protected readonly DbSet<BulletinRating> _dbSet;

    /// <inheritdoc/>
    public BulletinRatingRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinRating>();
    }

    /// <inheritdoc/>
    public async Task<BulletinRatingDto?> GetByIdAsync(Guid id)
    {
        BulletinRating? rating = await _context.BulletinRating
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        if (rating == null) return null;

        return _mapper.Map<BulletinRatingDto>(rating);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinRatingDto>> FindAsync(ExtendedSpecification<BulletinRating> specification)
    {
        IQueryable<BulletinRating> query = _dbSet.AsQueryable();

        query = query.ApplyExtendedSpecification(specification);

        return await query
            .Select(rating => _mapper.Map<BulletinRatingDto>(rating))
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<BulletinRatingDto> CreateAsync(BulletinRatingCreateDto ratingDto)
    {
        BulletinRating rating = _mapper.Map<BulletinRating>(ratingDto);
        EntityEntry<BulletinRating> ratingEntry = await _context.AddAsync(rating);
        rating = ratingEntry.Entity;
        return _mapper.Map<BulletinRatingDto>(rating);
    }

    /// <inheritdoc/>
    public async Task<BulletinRatingDto?> UpdateAsync(Guid id, BulletinRatingUpdateDto ratingDto)
    {
        BulletinRating? rating = await _context.BulletinRating
            .FirstOrDefaultAsync(r => r.Id == id);

        if (rating == null) return null;

        _mapper.Map(ratingDto, rating);

        return _mapper.Map<BulletinRatingDto>(rating);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        BulletinRating? rating = await _context.BulletinRating
            .FirstOrDefaultAsync(r => r.Id == id);

        if (rating == null) return false;

        _context.Remove(rating);
        return true;
    }
}