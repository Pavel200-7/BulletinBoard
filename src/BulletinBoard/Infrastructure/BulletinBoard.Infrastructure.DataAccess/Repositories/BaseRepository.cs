using AutoMapper;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.Extensions;
using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.DataAccess.Repositories;

/// <summary>
/// Базовый класс для умных репозиториев.
/// Содержит CRUD, поверх корогоро добавляются сложные запросы
/// </summary>
public abstract class BaseRepository
    <
    TEntity, 
    TDto, 
    TCreateDto, 
    TUpdateDto, 
    TContext
    >
    where TEntity : class
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
    where TContext : DbContext
{
    protected readonly IRepository<TEntity, TContext> _repository;
    protected readonly IMapper _mapper;

    protected BaseRepository(IRepository<TEntity, TContext> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<TDto?> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return null;
        return _mapper.Map<TDto>(entity);
    }

    /// <summary>
    /// Использует спецификацию для отбора,
    /// сортировки и пагинации.
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    public virtual async Task<IReadOnlyCollection<TDto>> FindAsync(ExtendedSpecification<TEntity> specification)
    {
        // Получение обертки - "Всех записей" для наложения запроса.
        var query = _repository.GetAll().AsQueryable();
        // Наложение запроса.
        query = query.ApplyExtendedSpecification(specification);
        // Применение запроса.
        return await query.Select(e => _mapper.Map<TDto>(e)).ToListAsync();
    }

    public virtual async Task<TDto> CreateAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity = await _repository.AddAsync(entity);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto?> UpdateAsync(Guid id, TUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return null;

        _mapper.Map(dto, entity);
        await _repository.UpdateAsync(entity);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}