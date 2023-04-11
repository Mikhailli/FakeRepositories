using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public record Series(int AnimeId, int SeriesNumber, int SeasonNumber, int SeriesDuration, string Link)
{
    public int AnimeId { get;} = AnimeId;

    public int SeriesNumber { get;} = SeriesNumber;

    public int SeasonNumber { get;} = SeasonNumber;

    public int SeriesDuration { get;} = SeriesDuration;

    public string Link { get;} = Link;
}