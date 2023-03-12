using System;
using System.Collections.Generic;
using FakeRepositories.Models;
using FakeRepositories.Repositories;

namespace FakeRepositories;

public static class CollectionExtensions
{
    public static FakeStudioRepository CreateRepository(this ICollection<Studio> collection)
    {
        if (collection is null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        return new FakeStudioRepository(collection);
    }
    
    public static FakeGenreRepository CreateRepository(this ICollection<Genre> collection)
    {
        if (collection is null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        return new FakeGenreRepository(collection);
    }

    public static FakeSeriesRepository CreateRepository(this ICollection<Series> collection,
        ICollection<Anime> animeCollection)
    {
        if (collection is null)
        {
            throw new ArgumentNullException(nameof(collection));
        }
        
        if (animeCollection is null)
        {
            throw new ArgumentNullException(nameof(animeCollection));
        }

        return new FakeSeriesRepository(collection, animeCollection);
    }
    
    public static FakeCharacterRepository CreateRepository(this ICollection<Character> collection,
        ICollection<Anime> animeCollection)
    {
        if (collection is null)
        {
            throw new ArgumentNullException(nameof(collection));
        }
        
        if (animeCollection is null)
        {
            throw new ArgumentNullException(nameof(animeCollection));
        }

        return new FakeCharacterRepository(collection, animeCollection);
    }
    
    public static FakeAnimeRepository CreateRepository(this ICollection<Anime> collection, 
        ICollection<Genre> genreCollection, ICollection<Studio> studioCollection)
    {
        if (collection is null)
        {
            throw new ArgumentNullException(nameof(collection));
        }
        
        if (genreCollection is null)
        {
            throw new ArgumentNullException(nameof(genreCollection));
        }
        
        if (studioCollection is null)
        {
            throw new ArgumentNullException(nameof(studioCollection));
        }

        return new FakeAnimeRepository(collection, genreCollection, studioCollection);
    }
}