using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FakeRepositories.Interfaces;

public abstract class GenericRepository<TEntity> where TEntity : Entity<int>
{
    public abstract TEntity GetById(object? id);
    
    public abstract Task<TEntity> GetByIdAsync(object id);
    
    public abstract IEnumerable<TEntity> GetAll();
    
    public abstract IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null);
    
    public abstract Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null);
    
    public abstract int GetCount(Expression<Func<TEntity, bool>>? predicate = null);

    public abstract TEntity Create();

    public abstract TEntity Add(TEntity entity);

    public abstract void Update(TEntity entity);

    public abstract void Delete(TEntity entity);

    public abstract void DeleteRange(TEntity[] entities);

    public abstract void RefreshAll();

    public abstract TEntity Clone(TEntity entity);
}