using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories.Models;

public class PlanningAnimeState : IAnimeState
{
    public Anime AddToPlanning(Anime anime)
    {
        Console.WriteLine("Аниме уже находится в запланированых");
        return anime;
    }

    public Anime AddToWatching(Anime anime)
    {
        Console.WriteLine("Аниме из запланированных перенесено в смотрю сейчас");
        anime.State = new WatchingAnimeState();
        return anime;
    }

    public Anime AddToWatched(Anime anime)
    {
        Console.WriteLine("Аниме из запланированных нельзя перенести в просмотренные");
        return anime;
    }

    public Anime AddToAbandoned(Anime anime)
    {
        Console.WriteLine("Аниме из запланированных нельзя перенести в заброшенные");
        return anime;
    }
}