using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Repositories;

/// <summary>
/// Сам глупый репозиторий.
/// Реализует соответствующий интерфейс.
/// Его реализация (определяемая generic полями) используется умными репозиториями.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TContext"></typeparam>
public class Repository<TEntity, TContext> : IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    protected readonly TContext DbContext;

    protected DbSet<TEntity> DbSet;

    public Repository(TContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }
    public IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity != null)
        {
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
