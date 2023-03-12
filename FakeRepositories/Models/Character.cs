using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Character : Entity<int>
{
    public int AnimeId { get; set; }

    public string Name { get; set; }

    public bool IsMainCharacter { get; set; }

    public string Photo { get; set; }

    public Character(int animeId, string name, string photo, bool isMainCharacter)
    {
        AnimeId = animeId;
        Name = name;
        Photo = photo;
        IsMainCharacter = isMainCharacter;
    }
}