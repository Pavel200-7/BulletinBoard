using AutoMapper;
using AutoMapper.QueryableExtensions;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.Extensions;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;
using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepository;

/// <inheritdoc/>
public class BulletinReposotory : IBulletinReposotory
{
    private BulletinContext _bulletinContext;
    private IMapper _autoMapper;
    private DbSet<BulletinMain> DbSetBulletinMain;

    /// <inheritdoc/>
    public BulletinReposotory
        (
        BulletinContext bulletinContext,
        IMapper autoMapper
        )
    {
        _bulletinContext = bulletinContext;
        _autoMapper = autoMapper;
        DbSetBulletinMain = bulletinContext.Set<BulletinMain>();
    }

    /// <inheritdoc/>
    public async Task<BulletinDto?> GetByIdAsync(Guid id)
    {
        BulletinDto? belletinDto = await DbSetBulletinMain
            .Where(b => b.Id == id)
            .Include(b => b.Category)
            .Include(b => b.Characteristics)
                .ThenInclude(c => c.Characteristic)
            .Include(b => b.Characteristics)
                .ThenInclude(c => c.CharacteristicValue)
            .Include(b => b.Images)
            .Include(b => b.ViewsCount)
            .ProjectTo<BulletinDto>(_autoMapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return belletinDto;  
    }

    public async Task<BulletinReadSingleDto?> GetByIdReadSingleAsync(Guid id)
    {
        BulletinReadSingleDto? belletinDto = await DbSetBulletinMain
            .Where(b => b.Id == id)
            .Include(b => b.Category)
            .Include(b => b.Characteristics)
                .ThenInclude(c => c.Characteristic)
            .Include(b => b.Characteristics)
                .ThenInclude(c => c.CharacteristicValue)
            .Include(b => b.Images)
            .Include(b => b.ViewsCount)
            .ProjectTo<BulletinReadSingleDto>(_autoMapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return belletinDto;
    }

    public async Task<IReadOnlyCollection<BulletinReadPagenatedItemDto>> GetBulletinsAsync(ExtendedSpecification<BulletinMain> specification)
    {

        var query = DbSetBulletinMain
            .Include(b => b.Ratings)
            .Include(b => b.ViewsCount)
            .Include(b => b.Images)
            .AsQueryable();

        query = query.ApplyExtendedSpecification(specification);

        return await query
            .Select(b => _autoMapper.Map<BulletinReadPagenatedItemDto>(b))
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(BulletinCreateDto createDto, CancellationToken cancellationToken)
    {
        var bulletinMain = _autoMapper.Map<BulletinMain>(createDto.BulletinMain);
        var bulletinMainEntyty = await _bulletinContext.BulletinMain.AddAsync(bulletinMain, cancellationToken);
        Guid bulletinMainId = bulletinMainEntyty.Entity.Id;

        foreach (var characteristicDto in createDto.CharacteristicComparisons)
        {
            var characteristic = _autoMapper.Map<BulletinCharacteristicComparison>(characteristicDto);
            characteristic.BulletinId = bulletinMainId;
            await _bulletinContext.BulletinCharacteristicСomparison.AddAsync(characteristic);
        }

        foreach (var imageDto in createDto.Images)
        {
            var image = _autoMapper.Map<BulletinImage>(imageDto);
            image.BulletinId = bulletinMainId;
            await _bulletinContext.BulletinImage.AddAsync(image);
        }

        var viewsCount = _autoMapper.Map<BulletinViewsCount>(createDto.ViewsCount);
        viewsCount.BulletinId = bulletinMainId;
        await _bulletinContext.BulletinViewsCount.AddAsync(viewsCount);

        return bulletinMainId;
    }
}
