using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Series : Entity<int>
{
    public int AnimeId { get; set; }

    public int SeriesNumber { get; set; }

    public int SeasonNumber { get; set; }

    public int SeriesDuration { get; set; }

    public string Link { get; set; }

    public Series(int seriesNumber, int seasonNumber, int seriesDuration, string link)
    {
        SeriesNumber = seriesNumber;
        SeasonNumber = seasonNumber;
        SeriesDuration = seriesDuration;
        Link = link;
    }
}