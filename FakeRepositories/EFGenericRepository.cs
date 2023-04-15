#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Interfaces;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FakeRepositories;

public class EFGenericRepository<TEntity> : GenericRepository<TEntity> where TEntity : Entity<int>
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public EFGenericRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    protected IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        if (includes is { Length: > 0 })
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }
        
        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return query;
    }

    public override TEntity GetById(object? id)
    {
        return id is null ? null! : _dbSet.Find(id)!;
    }
    
    public override Task<TEntity> GetByIdAsync(object id)
    {
        return _dbSet.FindAsync(id);
    }

    public override IEnumerable<TEntity> GetAll()
    {
        return GetQueryable().AsEnumerable();
    }

    public override IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null)
    {
        return GetQueryable(filter, orderBy, includes, skip, take).ToList();
    }
    
    public override Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null)
    {
        return GetQueryable(filter, orderBy, includes, skip, take).ToListAsync();
    }

    public override int GetCount(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return GetQueryable(predicate).Count();
    }

    public override TEntity Create()
    {
        return _dbSet.Create();
    }

    public override TEntity Add(TEntity entity)
    {
        return _dbSet.Add(entity);
    }

    public override void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public override void Delete(TEntity entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }

    public override void DeleteRange(TEntity[] entities)
    {
        foreach (var entity in entities)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
        }

        _dbSet.RemoveRange(entities);
    }

    public override void RefreshAll()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries<TEntity>())
        {
            entry.Reload();
        }
    }

    public override TEntity Clone(TEntity entity)
    {
        var clone = Create();
        Add(clone);
        var originalEntityValues = _dbContext.Entry(entity).CurrentValues;
        _dbContext.Entry(clone).CurrentValues.SetValues(originalEntityValues);

        return clone;
    }

    public void CommitChanges()
    {
        _dbContext.SaveChanges();
    }
}