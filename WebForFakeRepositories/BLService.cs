using FakeRepositories;
using FakeRepositories.Interfaces;
using Newtonsoft.Json;
using WebForFakeRepositories.Models;

namespace WebForFakeRepositories;

public class BLService
{
    private readonly IRepository<Anime> _animeRepository;
    private readonly IRepository<Series> _seriesRepository;
    private readonly UnitOfWork _unitOfWork;

    public BLService()
    {
        _unitOfWork = new UnitOfWork();
        _animeRepository = _unitOfWork.Animes;
        _seriesRepository = _unitOfWork.Series;
    }
    
    public string GetTimeToWatchAllSeries(string animeName)
    {
        var anime = _animeRepository.GetAll().FirstOrDefault(anime => anime.Title == animeName);
        var series = _seriesRepository.GetAll();
        
        var thisAnimeSeries = series.Where(s => s.AnimeId == anime.Id).ToList();

        var duration = thisAnimeSeries.Sum(s => s.SeriesDuration);

        return Utils.ConvertSecondsToString(duration);
    }
    
    public Series GetSeriesByNumber(string animeName, int seriesNumber)
    {
        var anime = _animeRepository.GetAll().FirstOrDefault(anime => anime.Title == animeName);
        var series = _seriesRepository.GetAll();
        
        var thisAnimeSeries = series.Where(s => s.AnimeId == anime.Id).ToList();
        
        var seriesCount = thisAnimeSeries.Count;

        if (seriesNumber > seriesCount || seriesNumber <= 0)
        {
            throw new ArgumentException(
                $"Невозможно получить серию номер {seriesNumber}, так как в аниме {anime.Title} всего {seriesCount} серий.");
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
    
    public List<Series> GetSeasonSeries(string animeName, int seasonNumber)
    {
        var anime = _animeRepository.GetAll().FirstOrDefault(anime => anime.Title == animeName);
        var series = _seriesRepository.GetAll();
        
        var thisAnimeSeries = series.Where(s => s.AnimeId == anime.Id).ToList();
        var lastSeasonNumber = thisAnimeSeries.Select(s => s.SeasonNumber).Max();

        if (seasonNumber > lastSeasonNumber || seasonNumber <= 0)
            return new List<Series>();

        return thisAnimeSeries.Where(s => s.SeasonNumber == seasonNumber).ToList();
    }
    
    public Series GetLastSeries(string animeName)
    {
        var anime = _animeRepository.GetAll().FirstOrDefault(anime => anime.Title == animeName);
        var series = _seriesRepository.GetAll();
        
        var thisAnimeSeries = series.Where(s => s.AnimeId == anime.Id).ToList();
        var lastSeasonNumber = thisAnimeSeries.Select(s => s.SeasonNumber).Max();
        var lastSeriesNumber = thisAnimeSeries.Where(s => s.SeasonNumber == lastSeasonNumber).Select(s => s.SeriesNumber).Max();

        return thisAnimeSeries.First(s => s.SeasonNumber == lastSeasonNumber && s.SeriesNumber == lastSeriesNumber);
    }
}
