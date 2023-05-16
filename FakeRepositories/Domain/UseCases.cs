using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Interfaces;
using FakeRepositories.Models;

namespace FakeRepositories.Domain;

public class UseCases
{
    private readonly BusinessLogic _businessLogic;

    public UseCases(IRepository<Anime> animeRepository, IRepository<Series> seriesRepository)
    {
        _businessLogic = new BusinessLogic(animeRepository, seriesRepository);
    }
    
    public string GetTimeToWatchAllSeries(string animeName)
    {
        return _businessLogic.GetTimeToWatchAllSeries(animeName);
    }
    
    public Series GetSeriesByNumber(string animeName, int seriesNumber)
    {
        return _businessLogic.GetSeriesByNumber(animeName, seriesNumber);
    }
    
    public List<Series> GetSeasonSeries(string animeName, int seasonNumber)
    {
        return _businessLogic.GetSeasonSeries(animeName, seasonNumber);
    }
    
    public Series GetLastSeries(string animeName)
    {
        return _businessLogic.GetLastSeries(animeName);
    }
}