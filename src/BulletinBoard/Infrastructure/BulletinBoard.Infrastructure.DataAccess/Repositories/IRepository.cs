using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Repositories;

public interface IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task DeleteAsync(Guid id);
}
