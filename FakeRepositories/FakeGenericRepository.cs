#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Interfaces;

namespace FakeRepositories;

public class FakeGenericRepository<TEntity> : GenericRepository<TEntity> where TEntity : Entity<int>
{
    public FakeGenericRepository(ICollection<TEntity> collection) : base(collection)
    {
        
    }

    public override TEntity GetById(int id)
    {
        if (_collection is null)
        {
            throw new ArgumentNullException(nameof(_collection));
        }

        if (_collection.Any(entity => entity.Id == id) is false)
        {
            throw new InvalidOperationException($"В коллекции осутствует элемент с id {id}");
        }
        
        return _collection.First(entity => entity.Id == id);
    }

    public override IEnumerable<TEntity> GetAll()
    {
        return _collection;
    }

    public override int GetCount()
    {
        if (_collection is null)
        {
            throw new ArgumentNullException(nameof(_collection));
        }
        
        return _collection.Count;
    }
}