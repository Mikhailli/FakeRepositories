using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Models;
using FakeRepositories.Models.Extensions;

namespace FakeRepositories;

internal class Program
{
    public static void Main(string[] args)
    {
        var context = new ApplicationContext();

        var animeRepository = new EFGenericRepository<Anime>(context);
        var characterRepository = new EFGenericRepository<Character>(context);
        var genreRepository = new EFGenericRepository<Genre>(context);
        var seriesRepository = new EFGenericRepository<Series>(context);
        var studioRepository = new EFGenericRepository<Studio>(context);

        var anime = animeRepository.GetAll().First();
        var characters = characterRepository.Get(character => character.AnimeId == anime.Id);
        var series = seriesRepository.Get(series => series.AnimeId == anime.Id);
        
        Console.WriteLine($@"Название {anime.Title}, возрастное ограничение {anime.AgeLimit}, количество серий {series.Count()}");
        Console.WriteLine(@"Главные персонажи:");
        foreach (var character in characters)
        {
            Console.WriteLine($@"{character.Name}");
        }
        Console.WriteLine(@"Жанры:");
        foreach (var genre in anime.Genres)
        {
            Console.WriteLine($@"{genre.Title}");
        }
        Console.WriteLine(@"Студии:");
        foreach (var studio in anime.Studios)
        {
            Console.WriteLine($@"{studio.Title}");
        }
        Console.WriteLine($@"Время необходимое для просмотра всего аниме: {anime.GetTimeToWatchAllSeries(series.ToList())}");
        Console.ReadKey();
        
        // Код для начального заполнения бд, сейчас не нужен так как бд уже заполнена
        /*var anime = new Anime("Врата штейна", true, "", "16+", "", 
            new List<Studio>(), new List<Genre>());
        animeRepository.Add(anime);
        animeRepository.CommitChanges();
        
        var studio = new Studio("White Fox", new List<Anime>{ anime });
        studioRepository.Add(studio);
        studioRepository.CommitChanges();
        
        var genre1 = new Genre("Драма", new List<Anime>{ anime });
        genreRepository.Add(genre1);
        genreRepository.CommitChanges();
        
        var genre2 = new Genre("Психологическое", new List<Anime>{ anime });
        genreRepository.Add(genre2);
        genreRepository.CommitChanges();

        var genre3 = new Genre("Триллер", new List<Anime>{ anime });
        genreRepository.Add(genre3);
        genreRepository.CommitChanges();
        
        var genre4 = new Genre("Фантастика", new List<Anime>{ anime });
        genreRepository.Add(genre4);
        genreRepository.CommitChanges();

        var character1 = new Character("Итару Хасида", "", true, anime);
        characterRepository.Add(character1);
        characterRepository.CommitChanges();
        
        var character2 = new Character("Курису Макисэ", "", true, anime);
        characterRepository.Add(character2);
        characterRepository.CommitChanges();
        
        var character3 = new Character("Ринтаро Окабэ", "", true, anime);
        characterRepository.Add(character3);
        characterRepository.CommitChanges();
        
        var character4 = new Character("Маюри Сина", "", true, anime);
        characterRepository.Add(character4);
        characterRepository.CommitChanges();

        var series1 = new Series(1, 1, 24 * 60, "", anime);
        seriesRepository.Add(series1);
        seriesRepository.CommitChanges();
        
        var series2 = new Series(2, 1, 24 * 60, "", anime);
        seriesRepository.Add(series2);
        seriesRepository.CommitChanges();
        var series3 = new Series(3, 1, 24 * 60, "", anime);
        seriesRepository.Add(series3);
        seriesRepository.CommitChanges();
        var series4 = new Series(4, 1, 24 * 60, "", anime);
        seriesRepository.Add(series4);
        seriesRepository.CommitChanges();
        
        var series5 = new Series(5, 1, 24 * 60, "", anime);
        seriesRepository.Add(series5);
        seriesRepository.CommitChanges();

        var series6 = new Series(6, 1, 24 * 60, "", anime);
        seriesRepository.Add(series6);
        seriesRepository.CommitChanges();

        var series7 = new Series(7, 1, 24 * 60, "", anime);
        seriesRepository.Add(series7);
        seriesRepository.CommitChanges();

        var series8 = new Series(8, 1, 24 * 60, "", anime);
        seriesRepository.Add(series8);
        seriesRepository.CommitChanges();

        var series9 = new Series(9, 1, 24 * 60, "", anime);
        seriesRepository.Add(series9);
        seriesRepository.CommitChanges();

        var series10 = new Series(10, 1, 24 * 60, "", anime);
        seriesRepository.Add(series10);
        seriesRepository.CommitChanges();

        var series11 = new Series(11, 1, 24 * 60, "", anime);
        seriesRepository.Add(series11);
        seriesRepository.CommitChanges();

        var series12 = new Series(12, 1, 24 * 60, "", anime);
        seriesRepository.Add(series12);
        seriesRepository.CommitChanges();

        var series13 = new Series(13, 1, 24 * 60, "", anime);
        seriesRepository.Add(series13);
        seriesRepository.CommitChanges();

        var series14 = new Series(14, 1, 24 * 60, "", anime);
        seriesRepository.Add(series14);
        seriesRepository.CommitChanges();

        var series15 = new Series(15, 1, 24 * 60, "", anime);
        seriesRepository.Add(series15);
        seriesRepository.CommitChanges();

        var series16 = new Series(16, 1, 24 * 60, "", anime);
        seriesRepository.Add(series16);
        seriesRepository.CommitChanges();

        var series17 = new Series(17, 1, 24 * 60, "", anime);
        seriesRepository.Add(series17);
        seriesRepository.CommitChanges();

        var series18 = new Series(18, 1, 24 * 60, "", anime);
        seriesRepository.Add(series18);
        seriesRepository.CommitChanges();
                
        var series19 = new Series(19, 1, 24 * 60, "", anime);
        seriesRepository.Add(series19);
        seriesRepository.CommitChanges();

        var series20 = new Series(20, 1, 24 * 60, "", anime);
        seriesRepository.Add(series20);
        seriesRepository.CommitChanges();

        var series21 = new Series(21, 1, 24 * 60, "", anime);
        seriesRepository.Add(series21);
        seriesRepository.CommitChanges();

        var series22 = new Series(22, 1, 24 * 60, "", anime);
        seriesRepository.Add(series22);
        seriesRepository.CommitChanges();

        var series23 = new Series(23, 1, 24 * 60, "", anime);
        seriesRepository.Add(series23);
        seriesRepository.CommitChanges();

        var series24 = new Series(24, 1, 24 * 60, "", anime);
        seriesRepository.Add(series24);
        seriesRepository.CommitChanges();

        anime.Genres = new List<Genre>
        {
            genre1,
            genre2,
            genre3,
            genre4
        };

        anime.Studios = new List<Studio>
        {
            studio
        };
        
        animeRepository.CommitChanges();
        
        Console.ReadKey();*/
    }
}