using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Models;

namespace FakeRepositories.Repositories;

public class FakeGenreRepository : FakeGenericRepository<Genre>
{
    public FakeGenreRepository(ICollection<Genre> collection) : base(collection)
    {
        
    }
    
    public override void Delete(Genre genre)
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