using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.Extensions;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepository;

/// <inheritdoc/>
public class BulletinUserRepository : BulletinBaseRepository, IBulletinUserRepository
{
    protected readonly DbSet<BulletinUser> _dbSet;

    /// <inheritdoc/>
    public BulletinUserRepository(BulletinContext context, IMapper mapper) : base(context, mapper)
    {
        _dbSet = context.Set<BulletinUser>();
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto?> GetByIdAsync(Guid id)
    {
        BulletinUser? user = await _context.BulletinUser
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return null;

        return _mapper.Map<BulletinUserDto>(user);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinUserDto>> FindAsync(ExtendedSpecification<BulletinUser> specification)
    {
        IQueryable<BulletinUser> query = _dbSet.AsQueryable();

        query = query.ApplyExtendedSpecification(specification);

        return await query
            .Select(user => _mapper.Map<BulletinUserDto>(user))
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto> CreateAsync(BulletinUserCreateDto userDto)
    {
        BulletinUser user = _mapper.Map<BulletinUser>(userDto);
        EntityEntry<BulletinUser> userEntry = await _context.AddAsync(user);
        await _context.SaveChangesAsync();
        user = userEntry.Entity;
        return _mapper.Map<BulletinUserDto>(user);
    }

    /// <inheritdoc/>
    public async Task<BulletinUserDto?> UpdateAsync(Guid id, BulletinUserUpdateDto userDto)
    {
        BulletinUser? user = await _context.BulletinUser
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return null;

        _mapper.Map(userDto, user);
        await _context.SaveChangesAsync();

        return _mapper.Map<BulletinUserDto>(user);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        BulletinUser? user = await _context.BulletinUser
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return false;

        _context.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}