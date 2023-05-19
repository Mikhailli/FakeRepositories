using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories.Models;

public class Studio : Entity<int>
{
    public string Title { get; set; }

    public virtual ICollection<Anime> Animes { get; set; }
    public virtual ICollection<int> AnimeIds { get; set; }

    public Studio(string title, List<Anime> animes)
    {
        Title = title;
        Animes = animes;
    }
    
    public Studio(string title, List<int> animeIds)
    {
        Title = title;
        AnimeIds = animeIds;
    }
    
    public Studio()
    {
        Animes = new List<Anime>();
    }
}