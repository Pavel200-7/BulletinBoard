using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;


public class UnitOfWorkBulletin : IUnitOfWorkBulletin
{
    private readonly BulletinContext _context;
    private IDbContextTransaction _transaction;
    private bool _disposed = false;

    public UnitOfWorkBulletin(
        BulletinContext context,
        IBulletinCategoryRepository categoryRepository,
        IBulletinCharacteristicComparisonRepository comparisonRepository,
        IBulletinCharacteristicRepository characteristicRepository,
        IBulletinCharacteristicValueRepository characteristicValueRepository,
        IBulletinImageRepository imageRepository,
        IBulletinMainRepository mainRepository,
        IBulletinRatingRepository ratingRepository,
        IBulletinUserRepository userRepository)
    {
        _context = context;
        _categoryRepository = categoryRepository;
        _comparisonRepository = comparisonRepository;
        _characteristicRepository = characteristicRepository;
        _characteristicValueRepository = characteristicValueRepository;
        _imageRepository = imageRepository;
        _mainRepository = mainRepository;
        _ratingRepository = ratingRepository;
        _userRepository = userRepository;
    }

    public IBulletinCategoryRepository _categoryRepository { get; }
    public IBulletinCharacteristicComparisonRepository _comparisonRepository { get; }
    public IBulletinCharacteristicRepository _characteristicRepository { get; }
    public IBulletinCharacteristicValueRepository _characteristicValueRepository { get; }
    public IBulletinImageRepository _imageRepository { get; }
    public IBulletinMainRepository _mainRepository { get; }
    public IBulletinRatingRepository _ratingRepository { get; }
    public IBulletinUserRepository _userRepository { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
        {
            return;
            throw new InvalidOperationException("Transaction already started");
        }
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            if (_transaction == null)
            {
                return;
                throw new InvalidOperationException("No transaction to commit");
            }

            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
            }
            _disposed = true;
        }
    }
}