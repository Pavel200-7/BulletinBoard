﻿using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Repositories;

public interface IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
