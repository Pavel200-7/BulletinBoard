using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.Extensions;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicName;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinCharacteristicRepository : BulletinBaseRepository, IBulletinCharacteristicRepository
{
    protected readonly DbSet<BulletinCharacteristic> _dbSet;

    /// <inheritdoc/>
    public BulletinCharacteristicRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinCharacteristic>();
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicDto?> GetByIdAsync(Guid id)
    {
        BulletinCharacteristic? characteristic = await _context.BulletinCharacteristic
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (characteristic == null) return null;

        return _mapper.Map<BulletinCharacteristicDto>(characteristic);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinCharacteristicDto>> FindAsync(ExtendedSpecification<BulletinCharacteristic> specification)
    {
        IQueryable<BulletinCharacteristic> query = _dbSet.AsQueryable();

        query = query.ApplyExtendedSpecification(specification);

        return await query
            .Select(characteristic => _mapper.Map<BulletinCharacteristicDto>(characteristic))
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicDto> CreateAsync(BulletinCharacteristicCreateDto characteristicDto)
    {
        BulletinCharacteristic characteristic = _mapper.Map<BulletinCharacteristic>(characteristicDto);
        EntityEntry<BulletinCharacteristic> characteristicEntry = await _context.AddAsync(characteristic);
        characteristic = characteristicEntry.Entity;
        return _mapper.Map<BulletinCharacteristicDto>(characteristic);
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicDto?> UpdateAsync(Guid id, BulletinCharacteristicUpdateDto characteristicDto)
    {
        BulletinCharacteristic? characteristic = await _context.BulletinCharacteristic
            .FirstOrDefaultAsync(c => c.Id == id);

        if (characteristic == null) return null;

        _mapper.Map(characteristicDto, characteristic);

        return _mapper.Map<BulletinCharacteristicDto>(characteristic);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        BulletinCharacteristic? characteristic = await _context.BulletinCharacteristic
            .FirstOrDefaultAsync(c => c.Id == id);

        if (characteristic == null) return false;

        _context.Remove(characteristic);
        return true;
    }
}