using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Repositories;

/// <summary>
/// Интерфейс глупого репозитория.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TContext"></typeparam>
public interface IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// Получить обертку "Все данные". В умных репозиториях позволяет создать сложный запрос
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Тут понятно
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(Guid id);

    /// <summary>
    /// Тут понятно
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Тут понятно
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Тут понятно
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
}
