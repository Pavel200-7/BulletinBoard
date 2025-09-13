using AutoMapper;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;

/// <summary>
/// Базовый класс для создания репозиториев домена Bulletin
/// </summary>
abstract public class BulletinBaseRepository
{
    protected readonly BulletinContext _context;
    protected readonly IMapper _mapper;

    /// <inheritdoc/>
    public BulletinBaseRepository(BulletinContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
