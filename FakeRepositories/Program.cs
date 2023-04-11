using System.Collections.Generic;
using System.Linq;
using FakeRepositories.Models;

namespace FakeRepositories;

internal class Program
{
    public static void Main(string[] args)
    {
        var studioCollection = new List<Studio>();
        var seriesCollection = new List<Series>();
        var genreCollection = new List<Genre>();
        var characterCollection = new List<Character>();
        var animeCollection = new List<Anime>();

        var genres1 = new List<Genre> { new Genre("Приключения"), new Genre("Драма") };
        var genres2 = new List<Genre> { new Genre("Романтика") };
        var genres3 = new List<Genre> { new Genre("Боевик") };
        var genres4 = new List<Genre> { new Genre("Повседневность") };
        var genres5 = new List<Genre> { new Genre("Школа") };

        var studios1 = new List<Studio> { new Studio("Toei Animation") };
        var studios6 = new List<Studio> { new Studio("Toei Animation") };
        var studiosEqual = studios1[0].Equals(studios6[0]);
        var studios2 = new List<Studio> { new Studio("A-1 Pictures") };
        var studios3 = new List<Studio> { new Studio("Production I.G") };
        var studios4 = new List<Studio> { new Studio("Kyoto Animation") };
        var studios5 = new List<Studio> { new Studio("J.C.Staff") };

        var anime1 = new Anime("One Piece", true, "empty", "10+", "link", genres1, studios1);
        var anime6 = new Anime("One Piece", true, "empty", "10+", "link", genres1, studios1);
        var areEqual = anime1.Equals(anime6);
        var anime2 = new Anime("Kaguya-sama", true, "empty", "16+", "link", genres2, studios2);
        var anime3 = new Anime("Pscyho-Pass", true, "empty", "16+", "link", genres3, studios3);
        var anime4 = new Anime("Melancholy of Haruhi Suzumiya", true, "empty", "14+", "link", genres4, studios4);
        var anime5 = new Anime("Tora-Dora", true, "empty", "12+", "link", genres5, studios5);

        var series1 = new Series(1, 1, 1, 2304568, "link");
        var series2 = new Series(2, 1, 1, 39256, "link");
        var series3 = new Series(3, 1, 1, 9123654, "link");
        var series4 = new Series(4, 1, 1, 64945, "link");
        var series5 = new Series(5, 1, 1, 47341, "link");

        var character1 = new Character(1, "Monkey D. Luffy", "photo", true);
        var character2 = new Character(1, "Gold D. Roger", "photo", true);
        var character3 = new Character(2, "Kaguya Shinomiya", "photo", true);
        var character4 = new Character(3, "Shinya Kougami", "photo", true);
        var character5 = new Character(4, "Kyon", "photo", true);
        var character6 = new Character(5, "Taiga Aisaka", "photo", true);

        System.Console.ReadLine();
    }
}