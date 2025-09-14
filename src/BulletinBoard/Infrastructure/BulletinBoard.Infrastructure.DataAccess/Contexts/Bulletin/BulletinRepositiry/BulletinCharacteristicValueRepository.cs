using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.Extensions;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinCharacteristicValueRepository : BulletinBaseRepository, IBulletinCharacteristicValueRepository
{
    protected readonly DbSet<BulletinCharacteristicValue> _dbSet;

    /// <inheritdoc/>
    public BulletinCharacteristicValueRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinCharacteristicValue>();
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicValueDto?> GetByIdAsync(Guid id)
    {
        BulletinCharacteristicValue? characteristicValue = await _context.BulletinCharacteristicValue
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (characteristicValue == null) return null;

        return _mapper.Map<BulletinCharacteristicValueDto>(characteristicValue);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinCharacteristicValueDto>> FindAsync(ExtendedSpecification<BulletinCharacteristicValue> specification)
    {
        IQueryable<BulletinCharacteristicValue> query = _dbSet.AsQueryable();

        query = query.ApplyExtendedSpecification(specification);

        return await query
            .Select(characteristicValue => _mapper.Map<BulletinCharacteristicValueDto>(characteristicValue))
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicValueDto> CreateAsync(BulletinCharacteristicValueCreateDto characteristicValueDto)
    {
        BulletinCharacteristicValue characteristicValue = _mapper.Map<BulletinCharacteristicValue>(characteristicValueDto);
        EntityEntry<BulletinCharacteristicValue> characteristicValueEntry = await _context.AddAsync(characteristicValue);
        characteristicValue = characteristicValueEntry.Entity;
        return _mapper.Map<BulletinCharacteristicValueDto>(characteristicValue);
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicValueDto?> UpdateAsync(Guid id, BulletinCharacteristicValueUpdateDto characteristicValueDto)
    {
        BulletinCharacteristicValue? characteristicValue = await _context.BulletinCharacteristicValue
            .FirstOrDefaultAsync(c => c.Id == id);

        if (characteristicValue == null) return null;

        _mapper.Map(characteristicValueDto, characteristicValue);

        return _mapper.Map<BulletinCharacteristicValueDto>(characteristicValue);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        BulletinCharacteristicValue? characteristicValue = await _context.BulletinCharacteristicValue
            .FirstOrDefaultAsync(c => c.Id == id);

        if (characteristicValue == null) return false;

        _context.Remove(characteristicValue);
        return true;
    }
}