using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Series : Entity<int>
{
    public int AnimeId { get; set; }

    public int SeriesNumber { get; set; }

    public int SeasonNumber { get; set; }

    public string SeriesDuration { get; set; }

    public string Link { get; set; }

    public Series(int animeId, int seriesNumber, int seasonNumber, string seriesDuration, string link)
    {
        AnimeId = animeId;
        SeriesNumber = seriesNumber;
        SeasonNumber = seasonNumber;
        SeriesDuration = seriesDuration;
        Link = link;
    }
}