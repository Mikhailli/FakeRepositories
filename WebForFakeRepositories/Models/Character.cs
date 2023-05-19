using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories.Models;

public class Character : Entity<int>
{
    public int AnimeId { get; set; }

    public Anime Anime { get; set; }

    public string Name { get; set; }

    public bool IsMainCharacter { get; set; }

    public string Photo { get; set; }
    
    public Character()
    {
        
    }

    public Character(string name, string photo, bool isMainCharacter)
    {
        Name = name;
        Photo = photo;
        IsMainCharacter = isMainCharacter;
    }
    
    public Character(string name, string photo, bool isMainCharacter, Anime anime)
    {
        Name = name;
        Photo = photo;
        IsMainCharacter = isMainCharacter;
        Anime = anime;
    }
}