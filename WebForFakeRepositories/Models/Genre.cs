using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories.Models;

public class Genre : Entity<int>
{
    public string Title { get; set; }

    public virtual ICollection<Anime> Animes { get; set; }
    
    public virtual ICollection<int> AnimeIds { get; set; }

    public Genre()
    {
        Animes = new List<Anime>();
    }
    
    public Genre(string title, List<Anime> animes)
    {
        Title = title;
        Animes = animes;
    }
}