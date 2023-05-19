using ConsoleApp1.JsonRepositories;
using FakeRepositories.Interfaces;
using WebForFakeRepositories.Interfaces;

namespace WebForFakeRepositories;

public static class RepositoryFabric<TEntity> where TEntity : Entity<int>
{
    private static string GetTypeName()
    {
        return typeof(TEntity).Name;
    }

    public static IRepository<TEntity>? CreateRepository(RepositoryType repositoryType)
    {
        return repositoryType switch
        {
            RepositoryType.Relational => new EFGenericRepository<TEntity>(new ApplicationContext()),
            RepositoryType.Xml => GetCorrectXmlRepository(),
            RepositoryType.Json => GetCorrectJsonRepository(),
            _ => throw new NotImplementedException()
        };
    }

    #region Private

    private static IRepository<TEntity>? GetCorrectXmlRepository()
    {
        var typeName = GetTypeName();

        return typeName switch
        {
            "Anime" => new XmlAnimeRepository() as IRepository<TEntity>,
            "Character" => new XmlCharacterRepository() as IRepository<TEntity>,
            "Genre" => new XmlGenreRepository() as IRepository<TEntity>,
            "Series" => new XmlSeriesRepository() as IRepository<TEntity>,
            _ => new XmlStudioRepository() as IRepository<TEntity>
        };
    }
    
    private static IRepository<TEntity>? GetCorrectJsonRepository()
    {
        var typeName = GetTypeName();

        return typeName switch
        {
            "Anime" => new JsonAnimeRepository() as IRepository<TEntity>,
            "Character" => new JsonCharacterRepository() as IRepository<TEntity>,
            "Genre" => new JsonGenreRepository() as IRepository<TEntity>,
            "Series" => new JsonSeriesRepository() as IRepository<TEntity>,
            _ => new JsonStudioRepository() as IRepository<TEntity>
        };
    }

    #endregion
}
