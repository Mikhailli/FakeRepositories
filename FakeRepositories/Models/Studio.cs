using System.Collections.Generic;
using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Studio : Entity<int>
{
    public string Title { get; set; }

    public virtual ICollection<Anime> Animes { get; set; }

    public Studio(string title, List<Anime> animes)
    {
        Title = title;
        Animes = animes;
    }
    
    public Studio()
    {
        Animes = new List<Anime>();
    }
}