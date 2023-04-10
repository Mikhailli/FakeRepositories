using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeRepositories.Interfaces;

public abstract class GenericRepository<TEntity> where TEntity : Entity<int>
{
    protected readonly ICollection<TEntity> _collection;

    protected GenericRepository(ICollection<TEntity> collection)
    {
        _collection = collection;
    }

    public virtual void AddMany(IEnumerable<TEntity> entities)
    {
        if (_collection is null)
        {
            throw new ArgumentNullException(nameof(_collection));
        }

        var id = 1;

        if (_collection.Any())
        {
            id = _collection.Max(element => element.Id) + 1;
        }
        foreach(var entity in entities)
        {
            entity.Id = id++;

            _collection.Add(entity);
        }

    }
    
    public abstract TEntity GetById(int id);

    public abstract IEnumerable<TEntity> GetAll();
    
    public abstract int GetCount();

    public virtual void Add(TEntity entity)
    {
        if (_collection is null)
        {
            throw new ArgumentNullException(nameof(_collection));
        }

        var id = 1;
        
        if (_collection.Any())
        {
            id = _collection.Max(element => element.Id) + 1;
        }
        
        entity.Id = id;
        
        _collection.Add(entity);
    }

    public virtual void Delete(TEntity genre)
    {
        if (_collection is null)
        {
            throw new ArgumentNullException(nameof(_collection));
        }

        if (_collection.Any(element => element.Id == genre.Id) is false)
        {
            throw new ArgumentException($"Сущность для удаления отсутствует.");
        }

        _collection.Remove(genre); 
    }
}