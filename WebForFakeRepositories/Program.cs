using Newtonsoft.Json;

namespace WebForFakeRepositories;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        
        var app = builder.Build();
        
        var service = new BLService();

        app.Map("/GetTimeToWatchAllSeries/{animeName}",
            (string animeName) => $"{JsonConvert.SerializeObject(service.GetTimeToWatchAllSeries(animeName))}");
        
        app.Map("/GetSeriesByNumber/{animeName}/{seriesNumber:int}",
            (string animeName, int seriesNumber) => $"{JsonConvert.SerializeObject(service.GetSeriesByNumber(animeName, seriesNumber), new JsonSerializerSettings() {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            })}");
        
        app.Map("/GetSeasonSeries/{animeName}/{seasonNumber:int}",
            (string animeName, int seasonNumber) => $"{JsonConvert.SerializeObject(service.GetSeasonSeries(animeName, seasonNumber), new JsonSerializerSettings() {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            })}");
        
        app.Map("/GetLastSeries/{animeName}",
            (string animeName) => $"{JsonConvert.SerializeObject(service.GetLastSeries(animeName), new JsonSerializerSettings() {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            })}");
 
        app.Run();
    }
}