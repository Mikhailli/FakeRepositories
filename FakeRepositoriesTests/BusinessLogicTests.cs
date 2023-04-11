using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Models;
using FakeRepositories.Models.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeRepositoriesTests;

[TestClass]
public class Tests
{
    [DataTestMethod]
    [DataRow(8, 3, "24 секунды")]
    [DataRow(1, 1, "1 секунда")]
    [DataRow(3, 2, "6 секунд")]
    [DataRow(60 * 8 + 8, 3, "24 минуты, 24 секунды")]
    [DataRow(60 * 1 + 1, 1, "1 минута, 1 секунда")]
    [DataRow(60 * 3 + 3, 2, "6 минут, 6 секунд")]
    [DataRow(3600 * 11 + 60 * 11 + 11, 2, "22 часа, 22 минуты, 22 секунды")]
    [DataRow(3600 * 1 + 60 * 1 + 1, 1, "1 час, 1 минута, 1 секунда")]
    [DataRow(3600 * 3 + 60 * 3 + 3, 2, "6 часов, 6 минут, 6 секунд")]
    [DataRow(3600 * 24 * 11 + 3600 * 11 + 60 * 11 + 11, 2, "22 дня, 22 часа, 22 минуты, 22 секунды")]
    [DataRow(3600 * 24 * 1 + 3600 * 1 + 60 * 1 + 1, 1, "1 день, 1 час, 1 минута, 1 секунда")]
    [DataRow(3600 * 24 * 3 + 3600 * 3 + 60 * 3 + 3, 2, "6 дней, 6 часов, 6 минут, 6 секунд")]
    public void GetTimeToWatchAllSeries_AnyDeclination_ReturnsCorrectString(int duration, int count, string expectedResult)
    {
        // Arrange
        var anime = CreateEmptyAnime();
        var series = CreateSeries(1, duration, count, 1);

        // Act
        var result = anime.GetTimeToWatchAllSeries(series);
        
        // Assert
        Assert.AreEqual(expectedResult, result);
    }
    
    [DataTestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [DataRow(int.MaxValue)]
    public void GetSeriesByNumber_IncorrectNumber_ThrowsException(int seriesNumberToFind)
    {
        // Arrange
        var anime = CreateEmptyAnime();
        var series = CreateSeries(1, 24 * 60, 20, 1);
        series.AddRange(CreateSeries(1, 24 * 60, 20, 2));
        series.AddRange(CreateSeries(1, 24 * 60, 20, 3));

        // Act
        var exception = Assert.ThrowsException<ArgumentException>(() => anime.GetSeriesByNumber(series, seriesNumberToFind));

        // Assert
        Assert.AreEqual($"Вы ввели отрицательный номер серии или невозможно получить серию номер {seriesNumberToFind}, так как в аниме {anime.Title} всего 60 серий.", exception.Message);
    }
    
    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void GetSeasonSeries_CorrectSeasonNumber_ReturnsCorrectSeries(int seasonNumber)
    {
        // Arrange
        var anime = CreateEmptyAnime();
        var series= new List<Series>();
        series.AddRange(CreateSeries(1, 24 * 60, 20, 1));
        series.AddRange(CreateSeries(1, 24 * 60, 20, 2));
        series.AddRange(CreateSeries(1, 24 * 60, 20, 3));

        // Act
        var result = anime.GetSeasonSeries(series, seasonNumber);

        // Assert
        var seriesWithCorrectSeason = CreateSeries(1, 24 * 60, 20, seasonNumber);
        Assert.IsTrue(CompareCollectionsOfSeries(result, seriesWithCorrectSeason));
    }
    
    [DataTestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    [DataRow(int.MaxValue)]
    public void GetSeasonSeries_IncorrectSeasonNumber_ThrowsException(int seasonNumber)
    {
        // Arrange
        var anime = CreateEmptyAnime();
        var series= new List<Series>();
        series.AddRange(CreateSeries(1, 24 * 60, 20, 1));
        series.AddRange(CreateSeries(1, 24 * 60, 20, 2));
        series.AddRange(CreateSeries(1, 24 * 60, 20, 3));

        // Act
        var exception = Assert.ThrowsException<ArgumentException>(() => anime.GetSeasonSeries(series, seasonNumber));

        // Assert
        Assert.AreEqual($"Вы ввели неположительный номер сезона или невозможно получить сезон номер {seasonNumber}, так как в аниме {anime.Title} всего 3 сезона.", exception.Message);
    }
    
    [TestMethod]
    public void GetLastSeries_ReturnsCorrectSeries()
    {
        // Arrange
        var anime = CreateEmptyAnime();
        var series = CreateSeries(1, 24 * 60, 20, 1);
        series.AddRange(CreateSeries(1, 24 * 60, 20, 2));

        // Act
        var result = anime.GetLastSeries(series);

        // Assert
        Assert.IsTrue(CompareSeries(result, new Series(1, 20, 2, 0, "")));
    }

    private Anime CreateEmptyAnime()
    {
        var anime = new Anime("", true, "", "", "", new List<Genre>(), new List<Studio>())
        {
            Id = 1
        };
        return anime;
    }

    private List<Series> CreateSeries(int animeId, int duration, int countOfSeries, int seasonNumber)
    {
        var series = new List<Series>();
        for (var i = 1; i <= countOfSeries; i++)
            series.Add(new Series(animeId, i, seasonNumber, duration, ""));
        
        return series;
    }

    private bool CompareSeries(Series series1, Series series2)
    {
        if (series1.AnimeId == series2.AnimeId &&
            series1.SeasonNumber == series2.SeasonNumber &&
            series1.SeriesNumber == series2.SeriesNumber)
            return true;

        return false;
    }

    private bool CompareCollectionsOfSeries(List<Series> series1, List<Series> series2)
    {
        if (series1.Select(s => s.SeasonNumber).Max() != series2.Select(s => s.SeasonNumber).Max())
        {
            return false;
        }

        if (series1.Count != series2.Count)
        {
            return false;
        }

        var check = false;
        
        foreach (var series in series1)
        {
            foreach (var s in series2)
            {
                if (CompareSeries(series, s))
                    check = true;
            }

            if (check is false)
            {
                return false;
            }

            check = false;
        }

        return true;
    }
}