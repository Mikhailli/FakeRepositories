using WebForFakeRepositories.Models;

namespace ConsoleApp1;

public static class ConvertJsonToModel
{
    public static List<Character> ConvertJsonToCharacters()
    {
        using var reader = new StreamReader("C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\Jsons\\Characters.json");
        var json = reader.ReadToEnd();
        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Character>>(json) ?? throw new InvalidOperationException();
    }
    
    public static List<Genre> ConvertJsonToGenres()
    {
        using var reader = new StreamReader("C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\Jsons\\Genres.json");
        var json = reader.ReadToEnd();
        var genres = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Genre>>(json);
        return genres!;
    }
    
    public static List<Series> ConvertJsonToSeries()
    {
        using var reader = new StreamReader("C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\Jsons\\Series.json");
        var json = reader.ReadToEnd();
        var series = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Series>>(json);
        return series!;
    }
    
    public static List<Studio> ConvertJsonToStudios()
    {
        using var reader = new StreamReader("C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\Jsons\\Studios.json");
        var json = reader.ReadToEnd();
        var studios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Studio>>(json);
        return studios!;
    }
    
    public static List<Anime> ConvertJsonToAnimes()
    {
        using var reader = new StreamReader("C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\Jsons\\Animes.json");
        var json = reader.ReadToEnd();
        var animes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Anime>>(json);
        return animes!;
    }
}