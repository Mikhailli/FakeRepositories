using WebForFakeRepositories.Models;

namespace WebForFakeRepositories.Interfaces;

public interface IAnimeState
{
    Anime AddToPlanning(Anime anime);
    Anime AddToWatching(Anime anime);
    Anime AddToWatched(Anime anime);
    Anime AddToAbandoned(Anime anime);
}