

namespace FakeRepositories.Models;

public record Character(int AnimeId, string Name, string Photo, bool IsMainCharacter)
{
    public int AnimeId { get; } = AnimeId;

    public string Name { get; } = Name;

    public bool IsMainCharacter { get; } = IsMainCharacter;

    public string Photo { get; } = Photo;
}