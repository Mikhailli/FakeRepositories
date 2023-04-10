using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Series : Entity<int>
{
    public int AnimeId { get;}

    public int SeriesNumber { get;}

    public int SeasonNumber { get;}

    public int SeriesDuration { get;}

    public string Link { get;}

    public Series(int animeId, int seriesNumber, int seasonNumber, int seriesDuration, string link)
    {
        AnimeId = animeId;
        SeriesNumber = seriesNumber;
        SeasonNumber = seasonNumber;
        SeriesDuration = seriesDuration;
        Link = link;
    }
}