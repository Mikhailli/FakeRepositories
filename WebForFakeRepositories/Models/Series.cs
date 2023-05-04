using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories.Models;

public class Series : Entity<int>
{
    public int AnimeId { get; set; }

    public Anime Anime { get; set; }

    public int SeriesNumber { get; set; }

    public int SeasonNumber { get; set; }

    public int SeriesDuration { get; set; }

    public string Link { get; set; }

    public Series()
    {
        
    }
    
    public Series(int seriesNumber, int seasonNumber, int seriesDuration, string link, Anime anime)
    {
        SeriesNumber = seriesNumber;
        SeasonNumber = seasonNumber;
        SeriesDuration = seriesDuration;
        Link = link;
        Anime = anime;
    }
}