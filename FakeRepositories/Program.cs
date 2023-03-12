using System.Collections.Generic;
using FakeRepositories.Interfaces;
using FakeRepositories.Models;

namespace FakeRepositories
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var studioCollection = new List<Studio>();
            var seriesCollection = new List<Series>();
            var genreCollection = new List<Genre>();
            var characterCollection = new List<Character>();
            var animeCollection = new List<Anime>();

            var studioRepository = studioCollection.CreateRepository();
            var genreRepository = genreCollection.CreateRepository();

            var genre1 = new Genre("Genre1");
            var genre2 = new Genre("Genre2");
            var genre3 = new Genre("Genre3");
            var genre4 = new Genre("Genre4");
            var genre5 = new Genre("Genre5");

            var studio1 = new Studio("studio1");
            var studio2 = new Studio("studio2");
            var studio3 = new Studio("studio3");
            var studio4 = new Studio("studio4");
            var studio5 = new Studio("studio5");

            studioRepository.Add(studio1);
            studioRepository.Add(studio2);
            studioRepository.Add(studio3);
            studioRepository.Add(studio4);
            studioRepository.Add(studio5);
            
            genreRepository.Add(genre1);
            genreRepository.Add(genre2);
            genreRepository.Add(genre3);
            genreRepository.Add(genre4);
            genreRepository.Add(genre5);
            
            var animeRepository = animeCollection.CreateRepository(genreCollection, studioCollection);
            
            var anime1 = new Anime("Anime1", true, "empty", "16+", "link",
                new List<int> { 1 }, new List<int> { 1 });
            var anime2 = new Anime("Anime2", true, "empty", "16+", "link",
                new List<int> { 2 }, new List<int> { 2 });
            var anime3 = new Anime("Anime3", true, "empty", "16+", "link",
                new List<int> { 3 }, new List<int> { 3 });
            var anime4 = new Anime("Anime4", true, "empty", "16+", "link",
                new List<int> { 4 }, new List<int> { 4 });
            var anime5 = new Anime("Anime5", true, "empty", "16+", "link",
                new List<int> { 5 }, new List<int> { 5 });
            
            animeRepository.Add(anime1);
            animeRepository.Add(anime2);
            animeRepository.Add(anime3);
            animeRepository.Add(anime4);
            animeRepository.Add(anime5);
            
            var seriesRepository = seriesCollection.CreateRepository(animeCollection);
            var characterRepository = characterCollection.CreateRepository(animeCollection);

            var series1 = new Series(1, 1, 1, "20 минут", "link");
            var series2 = new Series(2, 1, 1, "20 минут", "link");
            var series3 = new Series(3, 1, 1, "20 минут", "link");
            var series4 = new Series(4, 1, 1, "20 минут", "link");
            var series5 = new Series(5, 1, 1, "20 минут", "link");

            var character1 = new Character(1, "Name", "photo", true);
            var character2 = new Character(2, "Name", "photo", true);
            var character3 = new Character(3, "Name", "photo", true);
            var character4 = new Character(4, "Name", "photo", true);
            var character5 = new Character(5, "Name", "photo", true);

            seriesRepository.Add(series1);
            seriesRepository.Add(series2);
            seriesRepository.Add(series3);
            seriesRepository.Add(series4);
            seriesRepository.Add(series5);
            
            characterRepository.Add(character1);
            characterRepository.Add(character2);
            characterRepository.Add(character3);
            characterRepository.Add(character4);
            characterRepository.Add(character5);
            
            
        }
    }
}