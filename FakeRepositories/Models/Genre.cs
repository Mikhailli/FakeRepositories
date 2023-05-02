using System.Collections.Generic;
using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public sealed class Genre : Entity<int>
{
    public string Title { get; set; }

    public ICollection<int> AnimeIds { get; set; }

    public Genre(string title, List<int> animeIds)
    {
        Title = title;
        AnimeIds = animeIds;
    }
}