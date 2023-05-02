using System.Collections.Generic;
using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public sealed class Anime : Entity<int>
{
    public string Title { get; set; }

    public bool IsEnd { get; set; }

    public string Description { get; set; }

    public string AgeLimit { get; set; }

    public string Link { get; set; }

    public ICollection<int> StudiosIds { get; set; }

    public ICollection<int> GenresIds { get; set; }

    public Anime(string title, bool isEnd, string description, string ageLimit, string link, 
        List<int> studiosIds, List<int> genresIds)
    {
        Title = title;
        IsEnd = isEnd;
        Description = description;
        AgeLimit = ageLimit;
        Link = link;
        StudiosIds = studiosIds;
        GenresIds = genresIds;
    }
}