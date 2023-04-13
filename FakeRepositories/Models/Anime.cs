using System.Collections.Generic;
using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

// Переопределен метод Equals
public class Anime : Entity<int>
{
    public string Title { get; set; }

    public bool IsEnd { get; set; }

    public string Description { get; set; }

    public string AgeLimit { get; set; }

    public string Link { get; set; }

    public List<Genre> Genres { get; set; }
    public List<Studio> Studios { get; set; }

    public Anime(string title, bool isEnd, string description, string ageLimit, string link,
        List<Genre> genres, List<Studio> studios)
    {
        Title = title;
        IsEnd = isEnd;
        Description = description;
        AgeLimit = ageLimit;
        Link = link;
        Genres = genres;
        Studios = studios;
    }

    // Проверяет что сравниваемый объект тоже является объектом класса Anime
    public override bool Equals(object obj)
    {
        return obj is Anime anime && Equals(anime);
    }

    // Проверяет равенство всех свойств
    public bool Equals(Anime other)
    {
        var isGenresEquals = EqualGenres(other.Genres);
        var isStudiosEquals = EqualStudios(other.Studios);

        return Title == other.Title &&
               IsEnd == other.IsEnd &&
               Description == other.Description &&
               AgeLimit == other.AgeLimit &&
               Link == other.Link &&
               isGenresEquals &&
               isStudiosEquals;
    }

    // Для сравнения по хэшу
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Title != null ? Title.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ IsEnd.GetHashCode();
            hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (AgeLimit != null ? AgeLimit.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Link != null ? Link.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Genres != null ? Genres.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Studios != null ? Studios.GetHashCode() : 0);
            return hashCode;
        }
    }

    // Сравнивает свойства Genres
    private bool EqualGenres(List<Genre> genres)
    {
        if (genres is null && Genres is null)
        {
            return true;
        }

        if (genres is null)
        {
            return false;
        }

        if (genres.Count != Genres.Count)
        {
            return false;
        }
        
        for (var i = 0; i < genres.Count; i++)
        {
            if (genres[i].Equals(Genres[i]) is false)
            {
                return false;
            }
        }
        
        return true;
    }
    
    // Сравнивает свойства Studios
    private bool EqualStudios(List<Studio> studios)
    {
        if (studios is null && Studios is null)
        {
            return true;
        }

        if (studios is null)
        {
            return false;
        }

        if (studios.Count != Studios.Count)
        {
            return false;
        }
        
        for (var i = 0; i < studios.Count; i++)
        {
            if (studios[i].Equals(Studios[i]) is false)
            {
                return false;
            }
        }
        
        return true;
    }
}