using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeRepositories.Models.Extensions;

public static class BusinessLogic
{
    public static string GetTimeToWatchAllSeries(this Anime anime, List<Series> series)
    {
        var thisAnimeSeries = series.Where(s => s.AnimeId == anime.Id).ToList();

        var duration = thisAnimeSeries.Sum(s => s.SeriesDuration);

        return Utils.ConvertSecondsToString(duration);
    }
    
    public static Series GetSeriesByNumber(this Anime anime, List<Series> series, int seriesNumber)
    {
        var thisAnimeSeries = series.Where(s => s.AnimeId == anime.Id).ToList();
        
        var seriesCount = thisAnimeSeries.Count;

        if (seriesNumber > seriesCount || seriesNumber <= 0)
        {
            throw new ArgumentException(
                $"Вы ввели отрицательный номер серии или невозможно получить серию номер {seriesNumber}, так как в аниме {anime.Title} всего {seriesCount} серий.");
        }

        var seasonNumbers = thisAnimeSeries.Select(s => s.SeasonNumber).Distinct();

        var counter = 0;

        var seriesToReturn = series.First();
        foreach (var seasonNumber in seasonNumbers)
        {
            var currentSeasonSeries = thisAnimeSeries.Where(s => s.SeasonNumber == seasonNumber);
            foreach (var currentSeries in currentSeasonSeries)
            {
                counter++;
                if (counter == seriesNumber)
                {
                    seriesToReturn = currentSeries;
                    break;
                }
            }
        }

        return seriesToReturn;
    }
    
    public static List<Series> GetSeasonSeries(this Anime anime, List<Series> series, int seasonNumber)
    {
        var thisAnimeSeries = series.Where(s => s.AnimeId == anime.Id).ToList();
        var lastSeasonNumber = thisAnimeSeries.Select(s => s.SeasonNumber).Max();

        if (seasonNumber > lastSeasonNumber || seasonNumber <= 0)
            throw new ArgumentException(
                $"Вы ввели неположительный номер сезона или невозможно получить сезон номер {seasonNumber}, так как в аниме {anime.Title} всего {lastSeasonNumber} сезона.");

        return thisAnimeSeries.Where(s => s.SeasonNumber == seasonNumber).ToList();
    }
    
    public static Series GetLastSeries(this Anime anime, List<Series> series)
    {
        var thisAnimeSeries = series.Where(s => s.AnimeId == anime.Id).ToList();
        var lastSeasonNumber = thisAnimeSeries.Select(s => s.SeasonNumber).Max();
        var lastSeriesNumber = thisAnimeSeries.Where(s => s.SeasonNumber == lastSeasonNumber).Select(s => s.SeriesNumber).Max();

        return thisAnimeSeries.First(s => s.SeasonNumber == lastSeasonNumber && s.SeriesNumber == lastSeriesNumber);
    }
}