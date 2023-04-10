using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Character : Entity<int>
{
    public int AnimeId { get; }

    public string Name { get; }

    public bool IsMainCharacter { get; }

    public string Photo { get; }

    public Character(int animeId, string name, string photo, bool isMainCharacter)
    {
        AnimeId = animeId;
        Name = name;
        Photo = photo;
        IsMainCharacter = isMainCharacter;
    }
}