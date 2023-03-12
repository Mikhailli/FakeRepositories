using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Models;

namespace FakeRepositories.Repositories;

public class FakeAnimeRepository : FakeGenericRepository<Anime>
{
    private readonly IEnumerable<Genre> _genreCollection;
    private readonly IEnumerable<Studio> _studioCollection;

    public FakeAnimeRepository(ICollection<Anime> collection, IEnumerable<Genre> genreCollection,
        IEnumerable<Studio> studioCollection) : base(collection)
    {
        _genreCollection = genreCollection;
        _studioCollection = studioCollection;
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
    
    public override void Add(Anime anime)
    {
        if (_collection is null)
        {
            throw new ArgumentNullException(nameof(_collection));
        }

        foreach (var genreId in anime.GenreIds)
        {
            if (_genreCollection.Any(genre => genre.Id == genreId) is false)
            {
                throw new ArgumentException($"Не существует жанра с id {genreId}");
            }
        }
        
        foreach (var studioId in anime.StudioIds)
        {
            if (_studioCollection.Any(studio => studio.Id == studioId) is false)
            {
                throw new ArgumentException($"Не существует жанра с id {studioId}");
            }
        }
        
        var id = 1;
        
        if (_collection.Any())
        {
            id = _collection.Max(element => element.Id) + 1;
        }
        
        anime.Id = id;

        _collection.Add(anime); 
    }
}