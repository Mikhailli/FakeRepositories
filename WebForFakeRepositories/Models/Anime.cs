using FakeRepositories.Models;
using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories.Models;

public class Anime : Entity<int>
{
    public string Title { get; set; }

    public bool IsEnd { get; set; }

    public string Description { get; set; }

    public string AgeLimit { get; set; }

    public string Link { get; set; }

    public virtual ICollection<Studio> Studios { get; set; }

    public virtual ICollection<Genre> Genres { get; set; }
    
    public virtual ICollection<int> GenresIds { get; set; }
    public virtual ICollection<int> StudiosIds { get; set; }

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
    
    public Anime(string title, bool isEnd, string description, string ageLimit, string link, 
        ICollection<int> studiosIds, ICollection<int> genresIds)
    {
        Title = title;
        IsEnd = isEnd;
        Description = description;
        AgeLimit = ageLimit;
        Link = link;
        StudiosIds = studiosIds;
        GenresIds = genresIds;
    }
    
    public Anime()
    {
        Studios = new List<Studio>();
        Genres = new List<Genre>();
    }
}