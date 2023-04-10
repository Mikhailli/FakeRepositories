using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Models;

namespace FakeRepositories.Repositories;

public class FakeCharacterRepository : FakeGenericRepository<Character>
{
    private readonly ICollection<Anime> _animeCollection;

    public FakeCharacterRepository(ICollection<Character> collection, ICollection<Anime> animeCollection) : base(collection)
    {
        _animeCollection = animeCollection;
    }

    public override void Add(Character character)
    {
        if (_animeCollection is null)
        {
            throw new ArgumentNullException(nameof(_animeCollection));
        }

        if (_animeCollection.Any(anime => anime.Id == character.AnimeId) is false)
        {
            throw new ArgumentException($"Аниме с id {character.AnimeId} не существует");
        }

        var id = 1;
        
        if (_collection.Any())
        {
            id = _collection.Max(element => element.Id) + 1;
        }
        
        character.Id = id;
        
        _collection.Add(character);
    }
    
    public IEnumerable<Character> GetByAnimeId(int animeId)
    {
        if (_animeCollection is null)
        {
            throw new ArgumentNullException(nameof(_animeCollection));
        }

        if (_animeCollection.Any(anime => anime.Id == animeId) is false)
        {
            throw new ArgumentException($"Аниме с id {animeId} не существует");
        }

        var result = _collection.Where(character => character.AnimeId == animeId);

        return result;
    }
}