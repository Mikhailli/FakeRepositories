using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories.Models;

public class WatchedAnimeState : IAnimeState
{
    public Anime AddToPlanning(Anime anime)
    {
        Console.WriteLine("Аниме из просмотренных нельзя перенести в запланированные");
        return anime;
    }

    public Anime AddToWatching(Anime anime)
    {
        Console.WriteLine("Аниме из просмотренных нельзя перенести в смотрю сейчас");
        return anime;
    }

    public Anime AddToWatched(Anime anime)
    {
        Console.WriteLine("Аниме уже находится в просмотренных");
        return anime;
    }

    public Anime AddToAbandoned(Anime anime)
    {
        Console.WriteLine("Аниме из просмотренных нельзя перенести в заброшенные");
        return anime;
    }
}