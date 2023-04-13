namespace FakeRepositories.Models;

// Объект-значение
public record Genre(string Title)
{
    public string Title { get;} = Title;
}