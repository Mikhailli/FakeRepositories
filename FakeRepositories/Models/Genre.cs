using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Genre : Entity<int>
{
    public string Title { get;}

    public Genre(string title)
    {
        Title = title;
    }
}