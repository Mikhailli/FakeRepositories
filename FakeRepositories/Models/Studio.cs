namespace FakeRepositories.Models;

// Объект-значение
public record Studio(string Title)
{
    public string Title { get; } = Title;
}