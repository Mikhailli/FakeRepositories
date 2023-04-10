using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Models;

namespace FakeRepositories.Repositories;

public class FakeAnimeRepository : FakeGenericRepository<Anime>
{
    public FakeAnimeRepository(ICollection<Anime> collection, 
        ICollection<Genre> genreCollection, ICollection<Studio> studioCollection) : base(collection)
    {
    }

    public override void Delete(Anime genre)
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