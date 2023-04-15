using System.Collections.Generic;
using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Anime : Entity<int>
{
    public string Title { get; set; }

    public bool IsEnd { get; set; }

    public string Description { get; set; }

    public string AgeLimit { get; set; }

    public string Link { get; set; }

    public virtual ICollection<Studio> Studios { get; set; }

    public virtual ICollection<Genre> Genres { get; set; }

    public Anime(string title, bool isEnd, string description, string ageLimit, string link, 
        List<Studio> studios, List<Genre> genres)
    {
        Title = title;
        IsEnd = isEnd;
        Description = description;
        AgeLimit = ageLimit;
        Link = link;
        Studios = studios;
        Genres = genres;
    }
    
    public Anime()
    {
        Studios = new List<Studio>();
        Genres = new List<Genre>();
    }
}