using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories.Models;

public class WatchingAnimeState : IAnimeState
{
    public Anime AddToPlanning(Anime anime)
    {
        Console.WriteLine("Аниме из смотрю сейчас нельзя перенести в запланированные");
        return anime;
    }

    public Anime AddToWatching(Anime anime)
    {
        Console.WriteLine("Аниме уже находится в смотрю сейчас");
        return anime;
    }

    public Anime AddToWatched(Anime anime)
    {
        Console.WriteLine("Аниме из смотрю сейчас перенесено в просмотренные");
        anime.State = new WatchedAnimeState();
        return anime;
    }

    public Anime AddToAbandoned(Anime anime)
    {
        Console.WriteLine("Аниме из смотрю сейчас перенесено в заброшенные");
        anime.State = new AbandonedAnimeState();
        return anime;
    }
}