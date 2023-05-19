using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories.Models;

public class AbandonedAnimeState : IAnimeState
{
    public Anime AddToPlanning(Anime anime)
    {
        Console.WriteLine("Аниме из заброшенных перенесено в запланированные");
        anime.State = new PlanningAnimeState();
        return anime;
    }

    public Anime AddToWatching(Anime anime)
    {
        Console.WriteLine("Аниме из заброшенных перенесено в смотрю сейчас");
        anime.State = new WatchingAnimeState();
        return anime;
    }

    public Anime AddToWatched(Anime anime)
    {
        Console.WriteLine("Аниме из заброшенных нельзя перенести в просмотренные");
        return anime;
    }

    public Anime AddToAbandoned(Anime anime)
    {
        Console.WriteLine("Аниме уже находится в заброшенных");
        return anime;
    }
}