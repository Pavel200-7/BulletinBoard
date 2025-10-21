using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Repositories;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepository;

/// <inheritdoc/>
public class BulletinMainRepository :
    BaseRepository
    <
    BulletinMain,
    BulletinMainDto,
    BulletinMainCreateDto,
    BulletinMainUpdateDto,
    BulletinContext
    >,
    IBulletinMainRepository
{
    public BulletinMainRepository(IRepository<BulletinMain, BulletinContext> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }

    public async Task<BulletinMainDto?> SetBulletinBlockStatusAsync(Guid id, bool blockStatus, CancellationToken cancellation)
    {
        BulletinMain? bulletin = await _repository.GetByIdAsync(id);
        if (bulletin is null) { return null; }
        bulletin.Blocked = blockStatus;
        await _repository.UpdateAsync(bulletin, cancellation);
        return _mapper.Map<BulletinMainDto>(bulletin);
    }
}