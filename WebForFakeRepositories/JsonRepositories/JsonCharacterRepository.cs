using FakeRepositories.Interfaces;
using Newtonsoft.Json;
using WebForFakeRepositories.Models;

namespace ConsoleApp1.JsonRepositories;

public class JsonCharacterRepository : IRepository<Character>
{
    private const string Path =
        "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\Jsons\\Characters.json";

    private string _content;
    public IEnumerable<Character> GetAll()
    {
        return ConvertJsonToModel.ConvertJsonToCharacters();
    }

    public void Insert(Character character)
    {
        var allCharacters = GetAll().ToList();
        allCharacters.Add(character);
        _content = JsonConvert.SerializeObject(allCharacters);
        Save();
    }

    public void Update(Character character)
    {
        var allCharacters = GetAll().ToList();
        var ourCharacter = allCharacters.FirstOrDefault(ch => ch.Id == character.Id);

        if (ourCharacter is not null)
        {
            allCharacters.Remove(ourCharacter);
            allCharacters.Add(character);
        }
        
        _content = JsonConvert.SerializeObject(allCharacters);
        Save();
    }

    public void Delete(int id)
    {
        var allCharacters = GetAll().ToList();
        var ourCharacter = allCharacters.FirstOrDefault(ch => ch.Id == id);
        
        if (ourCharacter is not null) 
            allCharacters.Remove(ourCharacter);
        
        _content = JsonConvert.SerializeObject(allCharacters);
        Save();
    }

    public void Save()
    {
        File.WriteAllText(Path,_content);
    }
}