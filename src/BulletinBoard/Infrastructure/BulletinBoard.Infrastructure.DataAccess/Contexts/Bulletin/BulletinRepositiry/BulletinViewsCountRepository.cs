using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

public class BulletinViewsCountRepository : BaseRepository
    <
    BulletinViewsCount,
    BulletinViewsCountDto,
    BulletinViewsCountCreateDto,
    BulletinViewsCountUpdateDto,
    BulletinContext
    >, IBulletinViewsCountRepository
{
    public BulletinViewsCountRepository
        (
        IRepository<BulletinViewsCount, BulletinContext> repository
        , IMapper mapper
        ) : base(repository, mapper)
    {
    }

    public async Task<BulletinViewsCountDto?> IncreaseViewsCountAsync(Guid id, CancellationToken cancellationToken)
    {
        BulletinViewsCount? viewsCount = await _repository.GetAll()
            .Where(v => v.BulletinId == id)
            .FirstOrDefaultAsync();

        if (viewsCount == null) { return null; }

        viewsCount.ViewsCount++;
        viewsCount = await _repository.UpdateAsync(viewsCount, cancellationToken);
        return _mapper.Map<BulletinViewsCountDto>(viewsCount);
    }
}
