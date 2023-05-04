using ConsoleApp1.JsonRepositories;
using WebForFakeRepositories.Models;

var repo = new JsonAnimeRepository();
var anime = new Anime("Title", true, "", "17", "", new List<int>{1}, new List<int>{1, 2, 3}){Id = 2};
repo.Insert(anime);