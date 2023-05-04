using FakeRepositories.Interfaces;
using Newtonsoft.Json;
using WebForFakeRepositories.Models;

namespace ConsoleApp1.JsonRepositories;

public class JsonStudioRepository : IRepository<Studio>
{
    private const string Path =
        "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\Jsons\\Studios.json";

    private string _content;

    private readonly JsonAnimeRepository _animeRepository;
    
    public JsonStudioRepository(JsonAnimeRepository? animeRepository = null)
    {
        _animeRepository = animeRepository ?? new JsonAnimeRepository(null, this);
    }
    
    public IEnumerable<Studio> GetAll()
    {
        return ConvertJsonToModel.ConvertJsonToStudios();
    }

    public void Insert(Studio studio)
    {
        var allStudios = GetAll().ToList();
        allStudios.Add(studio);
        _content = JsonConvert.SerializeObject(allStudios);
        Save();
        
        foreach (var anime in GetAnimesByStudio(studio).Where(anime => anime.GenresIds.Contains(studio.Id) is false))
        {
            anime.GenresIds.Add(anime.Id);
            _animeRepository.Update(anime);
        }
    }

    public void Update(Studio studio)
    {
        var allStudios = GetAll().ToList();
        var ourStudio = allStudios.FirstOrDefault(s => s.Id == studio.Id);

        if (ourStudio is not null)
        {
            allStudios.Remove(ourStudio);
            allStudios.Add(studio);
        }
        
        _content = JsonConvert.SerializeObject(allStudios);
        Save();
        
        foreach (var anime in GetAnimesByStudio(studio).Where(anime => anime.GenresIds.Contains(studio.Id) is false))
        {
            anime.GenresIds.Add(anime.Id);
            _animeRepository.Update(anime);
        }
    }

    public void Delete(int id)
    {
        var allStudios = GetAll().ToList();
        var ourStudio = allStudios.FirstOrDefault(s => s.Id == id);
        
        if (ourStudio is not null) 
            allStudios.Remove(ourStudio);
        
        _content = JsonConvert.SerializeObject(allStudios);
        Save();
    }

    public void Save()
    {
        File.WriteAllText(Path,_content);
    }
    
    private List<Anime> GetAnimesByStudio(Studio studio)
    {
        var allAnimes = _animeRepository.GetAll();
        return allAnimes.Where(anime => studio.AnimeIds.Contains(anime.Id)).ToList();
    }
}