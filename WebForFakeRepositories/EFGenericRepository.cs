#nullable enable
using System.Data.Entity;
using System.Linq.Expressions;
using FakeRepositories.Interfaces;
using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories;

public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : Entity<int>
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

    public TEntity GetById(object? id)
    {
        return id is null ? null! : _dbSet.Find(id)!;
    }
    
    public Task<TEntity> GetByIdAsync(object id)
    {
        return _dbSet.FindAsync(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return GetQueryable().AsEnumerable();
    }

    public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null)
    {
        return GetQueryable(filter, orderBy, includes, skip, take).ToList();
    }
    
    public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null,
        int? take = null)
    {
        return GetQueryable(filter, orderBy, includes, skip, take).ToListAsync();
    }

    public int GetCount(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return GetQueryable(predicate).Count();
    }

    public TEntity Create()
    {
        return _dbSet.Create();
    }

    public void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }

    public void Delete(int id)
    {
        Delete(GetById(id));
    }
}