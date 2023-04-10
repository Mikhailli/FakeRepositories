using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Models;

namespace FakeRepositories.Repositories;

public class FakeSeriesRepository : FakeGenericRepository<Series>
{
    private readonly ICollection<Anime> _animeCollection;

    public FakeSeriesRepository(ICollection<Series> collection, ICollection<Anime> animeCollection) : base(collection)
    {
        _animeCollection = animeCollection;
    }

    public override void Add(Series series)
    {
        if (_animeCollection is null)
        {
            throw new ArgumentNullException(nameof(_animeCollection));
        }

        if (_animeCollection.Any(anime => anime.Id == series.AnimeId) is false)
        {
            throw new ArgumentException($"Аниме с id {series.AnimeId} не существует");
        }
        
        var id = 1;
        
        if (_collection.Any())
        {
            id = _collection.Max(element => element.Id) + 1;
        }
        
        series.Id = id;
        
        _collection.Add(series);
    }
    
    public IEnumerable<Series> GetByAnimeId(int animeId)
    {
        if (_animeCollection is null)
        {
            throw new ArgumentNullException(nameof(_animeCollection));
        }

        if (_animeCollection.Any(anime => anime.Id == animeId) is false)
        {
            throw new ArgumentException($"Аниме с id {animeId} не существует");
        }

        var result = _collection.Where(series => series.AnimeId == animeId);

        return result;
    }
}