using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.Extensions;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonRepository : BulletinBaseRepository, IBulletinCharacteristicComparisonRepository
{
    protected readonly DbSet<BulletinCharacteristicComparison> _dbSet;

    /// <inheritdoc/>
    public BulletinCharacteristicComparisonRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinCharacteristicComparison>();
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicComparisonDto?> GetByIdAsync(Guid id)
    {
        BulletinCharacteristicComparison? comparison = await _context.BulletinCharacteristicСomparison
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comparison == null) return null;

        return _mapper.Map<BulletinCharacteristicComparisonDto>(comparison);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinCharacteristicComparisonDto>> FindAsync(ExtendedSpecification<BulletinCharacteristicComparison> specification)
    {
        IQueryable<BulletinCharacteristicComparison> query = _dbSet.AsQueryable();

        query = query.ApplyExtendedSpecification(specification);

        return await query
            .Select(comparison => _mapper.Map<BulletinCharacteristicComparisonDto>(comparison))
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicComparisonDto> CreateAsync(BulletinCharacteristicComparisonCreateDto comparisonDto)
    {
        BulletinCharacteristicComparison comparison = _mapper.Map<BulletinCharacteristicComparison>(comparisonDto);
        EntityEntry<BulletinCharacteristicComparison> comparisonEntry = await _context.AddAsync(comparison);
        comparison = comparisonEntry.Entity;
        return _mapper.Map<BulletinCharacteristicComparisonDto>(comparison);
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicComparisonDto?> UpdateAsync(Guid id, BulletinCharacteristicComparisonUpdateDto comparisonDto)
    {
        BulletinCharacteristicComparison? comparison = await _context.BulletinCharacteristicСomparison
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comparison == null) return null;

        _mapper.Map(comparisonDto, comparison);

        return _mapper.Map<BulletinCharacteristicComparisonDto>(comparison);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        BulletinCharacteristicComparison? comparison = await _context.BulletinCharacteristicСomparison
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comparison == null) return false;

        _context.Remove(comparison);
        return true;
    }
}