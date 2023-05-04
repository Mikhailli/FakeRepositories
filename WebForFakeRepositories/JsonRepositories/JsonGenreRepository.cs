using FakeRepositories.Interfaces;
using Newtonsoft.Json;
using WebForFakeRepositories.Models;

namespace ConsoleApp1.JsonRepositories;

public class JsonGenreRepository : IRepository<Genre>
{
    private const string Path =
        "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\Jsons\\Genres.json";

    private string _content;

    private JsonAnimeRepository _animeRepository;
    
    public JsonGenreRepository(JsonAnimeRepository? animeRepository = null)
    {
        _animeRepository = animeRepository ?? new JsonAnimeRepository(this, null);
    }
    
    public IEnumerable<Genre> GetAll()
    {
        return ConvertJsonToModel.ConvertJsonToGenres();
    }

    public void Insert(Genre genre)
    {
        var allGenres = GetAll().ToList();
        allGenres.Add(genre);
        _content = JsonConvert.SerializeObject(allGenres);
        Save();
        
        foreach (var anime in GetAnimesByGenre(genre))
        {
            if (anime.GenresIds.Contains(genre.Id) is false)
            {
                anime.GenresIds.Add(anime.Id);
                _animeRepository.Update(anime);
            }
        }
    }

    public void Update(Genre genre)
    {
        var allGenres = GetAll().ToList();
        var ourGenre = allGenres.FirstOrDefault(g => g.Id == genre.Id);

        if (ourGenre is not null)
        {
            allGenres.Remove(ourGenre);
            allGenres.Add(genre);
        }
        
        _content = JsonConvert.SerializeObject(allGenres);
        Save();
        
        foreach (var anime in GetAnimesByGenre(genre))
        {
            if (anime.GenresIds.Contains(genre.Id) is false)
            {
                anime.GenresIds.Add(anime.Id);
                _animeRepository.Update(anime);
            }
        }
    }

    public void Delete(int id)
    {
        var allGenres = GetAll().ToList();
        var ourGenre = allGenres.FirstOrDefault(ch => ch.Id == id);
        
        if (ourGenre is not null) 
            allGenres.Remove(ourGenre);
        
        _content = JsonConvert.SerializeObject(allGenres);
        Save();
    }

    public void Save()
    {
        File.WriteAllText(Path,_content);
    }
    
    private List<Anime> GetAnimesByGenre(Genre genre)
    {
        var allAnimes = _animeRepository.GetAll();
        return allAnimes.Where(anime => genre.AnimeIds.Contains(anime.Id)).ToList();
    }
}