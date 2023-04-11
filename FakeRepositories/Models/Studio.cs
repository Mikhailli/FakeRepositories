using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public record Studio(string Title)
{
    public string Title { get; } = Title;
}