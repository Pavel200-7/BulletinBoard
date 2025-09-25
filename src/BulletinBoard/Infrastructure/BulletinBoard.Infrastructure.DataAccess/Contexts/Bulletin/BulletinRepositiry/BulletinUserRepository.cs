using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser.UpdateDto;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using System.Numerics;
using System.Xml.Linq;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepository;

/// <inheritdoc/>
public class BulletinUserRepository :
    BaseRepository
    <
    BulletinUser,
    BulletinUserDto,
    BulletinUserCreateDto,
    BulletinUserUpdateDto,
    BulletinContext
    >,
    IBulletinUserRepository
{
    public BulletinUserRepository(IRepository<BulletinUser, BulletinContext> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }

    public async Task<BulletinUserDto?> SetUserBlockStatusAsync(Guid id, bool blockStatus, CancellationToken cancellation)
    {
        BulletinUser? user = await _repository.GetByIdAsync(id);
        if (user is null) { return null; }
        user.Blocked = blockStatus;
        await _repository.UpdateAsync(user, cancellation);
        return _mapper.Map<BulletinUserDto>(user);
    }

    public async Task<BulletinUserDto?> ChangeNameAsync(Guid id, string name, CancellationToken cancellation)
    {
        BulletinUser? user = await _repository.GetByIdAsync(id);
        if (user is null) { return null; }
        user.FullName = name;
        await _repository.UpdateAsync(user, cancellation);
        return _mapper.Map<BulletinUserDto>(user);
    }

    public async Task<BulletinUserDto?> ChangeAdressAsync(Guid id, BulletinUserUpdateLocationDto userLocationDto, CancellationToken cancellation)
    {
        BulletinUser? user = await _repository.GetByIdAsync(id);
        if (user is null) { return null; }
        user = _mapper.Map<BulletinUser>(userLocationDto);
        await _repository.UpdateAsync(user, cancellation);
        return _mapper.Map<BulletinUserDto>(user);
    }



    public async Task<BulletinUserDto?> ChangePhoneAsync(Guid id, string phone, CancellationToken cancellationToken)
    {
        BulletinUser? user = await _repository.GetByIdAsync(id);
        if (user is null) { return null; }
        user.Phone = phone;
        await _repository.UpdateAsync(user, cancellationToken);
        return _mapper.Map<BulletinUserDto>(user);
    }
}