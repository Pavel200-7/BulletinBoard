namespace BulletinBoard.AppServices.Specification.Extensions;

/// <summary>
/// Расширения для применения спецификаций к IQueryable.
/// Позволяют удобно применять расширенные спецификации и базовые фильтры к запросам данных.
/// 
/// Используется в репозиториях для преобразования спецификации в 
/// запрос с испозозованием dbSet.
/// </summary>
/// <remarks>
/// Пример использования:
///
///     public async Task{IReadOnlyCollection{{BulletinCategoryDto}} FindAsync(ExtendedSpecification{BulletinCategory} specification)
///     {
///         var query = _dbSet.AsQueryable();
///
///         query = query.ApplyExtendedSpecification(specification);
///
///         return await query
///             .Select(bulletinCategory => _mapper.Map{BulletinCategoryDto}(bulletinCategory))
///             .ToListAsync();
///     }
/// </remarks>
public static class SpecificationExtensions
{
    /// <summary>
    /// Применяет расширенную спецификацию к исходному IQueryable, добавляя фильтрацию, сортировку и пагинацию.
    /// </summary>
    /// <typeparam name="T">Тип элементов коллекции.</typeparam>
    /// <param name="query">Исходный запрос, к которому необходимо применить спецификацию.</param>
    /// <param name="specification">Расширенная спецификация, содержащая условия, сортировку и пагинацию.</param>
    /// <returns>Обновлённый IQueryable с применёнными условиями.</returns>
    public static IQueryable<T> ApplyExtendedSpecification<T>(this IQueryable<T> query, ExtendedSpecification<T> specification)
        where T : class
    {
        if (specification == null)
            return query;

        query = query.Where(specification.ToExpression());


        IOrderedQueryable<T> orderedQuery = null;

        bool isFirstOrderBy = true;
        foreach (var orderByItem in specification.OrderByList)
        {
            if (isFirstOrderBy)
            {
                orderedQuery = orderByItem.OrderByAscending
                    ? query.OrderBy(orderByItem.OrderByExpression)
                    : query.OrderByDescending(orderByItem.OrderByExpression);
                isFirstOrderBy = false;
            }
            else
            {
                orderedQuery = orderByItem.OrderByAscending
                    ? orderedQuery!.ThenBy(orderByItem.OrderByExpression)
                    : orderedQuery!.ThenByDescending(orderByItem.OrderByExpression);
            }
        }
        query = orderedQuery ?? query;


        if (specification.Skip.HasValue)
            query = query.Skip(specification.Skip.Value);

        if (specification.Take.HasValue)
            query = query.Take(specification.Take.Value);

        return query;
    }

    /// <summary>
    /// Применяет базовую спецификацию (фильтр) к исходному IQueryable.
    /// </summary>
    /// <typeparam name="T">Тип элементов коллекции.</typeparam>
    /// <param name="query">Исходный запрос.</param>
    /// <param name="specification">Спецификация, содержащая условие фильтрации.</param>
    /// <returns>Обновлённый IQueryable с применённым фильтром.</returns>
    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Specification<T> specification)
        where T : class
    {
        return query.Where(specification.ToExpression());
    }
}