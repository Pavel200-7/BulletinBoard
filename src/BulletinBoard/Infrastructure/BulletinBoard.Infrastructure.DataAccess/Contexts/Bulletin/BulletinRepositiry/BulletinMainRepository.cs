using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.Extensions;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinMainRepository : BulletinBaseRepository, IBulletinMainRepository
{
    protected readonly DbSet<BulletinMain> _dbSet;

    /// <inheritdoc/>
    public BulletinMainRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinMain>();
    }

    /// <inheritdoc/>
    public async Task<BulletinMainDto?> GetByIdAsync(Guid id)
    {
        BulletinMain? bulletin = await _context.BelletinMain
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (bulletin == null) return null;

        return _mapper.Map<BulletinMainDto>(bulletin);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinMainDto>> FindAsync(ExtendedSpecification<BulletinMain> specification)
    {
        IQueryable<BulletinMain> query = _dbSet.AsQueryable();

        query = query.ApplyExtendedSpecification(specification);

        return await query
            .Select(bulletin => _mapper.Map<BulletinMainDto>(bulletin))
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<BulletinMainDto> CreateAsync(BulletinMainCreateDto bulletinDto)
    {
        BulletinMain bulletin = _mapper.Map<BulletinMain>(bulletinDto);
        EntityEntry<BulletinMain> bulletinEntry = await _context.AddAsync(bulletin);
        bulletin = bulletinEntry.Entity;
        return _mapper.Map<BulletinMainDto>(bulletin);
    }

    /// <inheritdoc/>
    public async Task<BulletinMainDto?> UpdateAsync(Guid id, BulletinMainUpdateDto bulletinDto)
    {
        BulletinMain? bulletin = await _context.BelletinMain
            .FirstOrDefaultAsync(b => b.Id == id);

        if (bulletin == null) return null;

        _mapper.Map(bulletinDto, bulletin);

        return _mapper.Map<BulletinMainDto>(bulletin);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        BulletinMain? bulletin = await _context.BelletinMain
            .FirstOrDefaultAsync(b => b.Id == id);

        if (bulletin == null) return false;

        _context.Remove(bulletin);
        return true;
    }
}