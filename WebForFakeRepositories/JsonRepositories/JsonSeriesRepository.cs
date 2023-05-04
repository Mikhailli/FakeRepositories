using FakeRepositories.Interfaces;
using Newtonsoft.Json;
using WebForFakeRepositories.Models;

namespace ConsoleApp1.JsonRepositories;

public class JsonSeriesRepository : IRepository<Series>
{
    private const string Path =
        "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\Jsons\\Series.json";

    private string _content;
    public IEnumerable<Series> GetAll()
    {
        return ConvertJsonToModel.ConvertJsonToSeries();
    }

    public void Insert(Series serie)
    {
        var allSeries = GetAll().ToList();
        allSeries.Add(serie);
        _content = JsonConvert.SerializeObject(allSeries);
        Save();
    }

    public void Update(Series serie)
    {
        var allSeries = GetAll().ToList();
        var ourSerie = allSeries.FirstOrDefault(s => s.Id == serie.Id);

        if (ourSerie is not null)
        {
            allSeries.Remove(ourSerie);
            allSeries.Add(serie);
        }
        
        _content = JsonConvert.SerializeObject(allSeries);
        Save();
    }

    public void Delete(int id)
    {
        var allSeries = GetAll().ToList();
        var ourSerie = allSeries.FirstOrDefault(s => s.Id == id);
        
        if (ourSerie is not null) 
            allSeries.Remove(ourSerie);
        
        _content = JsonConvert.SerializeObject(allSeries);
        Save();
    }

    public void Save()
    {
        File.WriteAllText(Path,_content);
    }
}