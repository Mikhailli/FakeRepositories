using System;
using FakeRepositories.Domain;
using FakeRepositories.Interfaces;

namespace FakeRepositories;

internal static class Program
{
    public static void Main(string[] args)
    {
        var animeRepository = new XmlAnimeRepository();
        var seriesRepository = new XmlSeriesRepository();
        var useCases = new UseCases(animeRepository, seriesRepository);
        Console.WriteLine(useCases.GetTimeToWatchAllSeries("Врата Штейна"));
    }
}