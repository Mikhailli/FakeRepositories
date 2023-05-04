using FakeRepositories.Interfaces;
using Newtonsoft.Json;
using WebForFakeRepositories.Models;

namespace ConsoleApp1.JsonRepositories;

public class JsonAnimeRepository : IRepository<Anime>
{
    private const string Path =
        "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\Jsons\\Animes.json";

    private string _content;
    
    private JsonGenreRepository? _genreRepository;
    private JsonStudioRepository? _studioRepository; 
    
    public JsonAnimeRepository(JsonGenreRepository? genreRepository = null, JsonStudioRepository? studioRepository = null)
    {
        if (genreRepository is null && studioRepository is null)
        {
            _genreRepository = new JsonGenreRepository(this);
            _studioRepository = new JsonStudioRepository(this);
        }
        else if (genreRepository is not null && studioRepository is null)
        {
            _genreRepository = genreRepository;
            _studioRepository = new JsonStudioRepository(this);
        }
        else
        {
            _genreRepository = new JsonGenreRepository(this);
            _studioRepository = studioRepository;
        }
    }
    
    public IEnumerable<Anime> GetAll()
    {
        return ConvertJsonToModel.ConvertJsonToAnimes();
    }

    public void Insert(Anime anime)
    {
        var allAnimes = GetAll().ToList();
        allAnimes.Add(anime);
        _content = JsonConvert.SerializeObject(allAnimes);
        Save();
        
        foreach (var genre in GetGenresByAnime(anime))
        {
            if (genre.AnimeIds.Contains(anime.Id) is false)
            {
                genre.AnimeIds.Add(anime.Id);
                _genreRepository.Update(genre);
            }
        }
        
        foreach (var studio in GetStudiosByAnime(anime))
        {
            if (studio.AnimeIds.Contains(anime.Id) is false)
            {
                studio.AnimeIds.Add(anime.Id);
                _studioRepository.Update(studio);
            }
        }
    }

    public void Update(Anime anime)
    {
        var allAnimes = GetAll().ToList();
        var ourAnime = allAnimes.FirstOrDefault(s => s.Id == anime.Id);

        if (ourAnime is not null)
        {
            allAnimes.Remove(ourAnime);
            allAnimes.Add(anime);
        }
        
        _content = JsonConvert.SerializeObject(allAnimes);
        Save();
        
        foreach (var genre in GetGenresByAnime(anime))
        {
            if (genre.AnimeIds.Contains(anime.Id) is false)
            {
                genre.AnimeIds.Add(anime.Id);
                _genreRepository.Update(genre);
            }
        }
        
        foreach (var studio in GetStudiosByAnime(anime))
        {
            if (studio.AnimeIds.Contains(anime.Id) is false)
            {
                studio.AnimeIds.Add(anime.Id);
                _studioRepository.Update(studio);
            }
        }
    }

    public void Delete(int id)
    {
        var allAnimes = GetAll().ToList();
        var ourAnimes = allAnimes.FirstOrDefault(s => s.Id == id);
        
        if (ourAnimes is not null) 
            allAnimes.Remove(ourAnimes);
        
        _content = JsonConvert.SerializeObject(allAnimes);
        Save();
    }

    public void Save()
    {
        File.WriteAllText(Path,_content);
    }
    
    private List<Genre> GetGenresByAnime(Anime anime)
    {
        var allGenres = _genreRepository.GetAll();
        return allGenres.Where(genre => anime.GenresIds.Contains(genre.Id)).ToList();
    }
    
    private List<Studio> GetStudiosByAnime(Anime anime)
    {
        var allStudios = _studioRepository.GetAll();
        return allStudios.Where(studio => anime.GenresIds.Contains(studio.Id)).ToList();
    }
}