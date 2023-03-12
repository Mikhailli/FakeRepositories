#nullable enable
namespace FakeRepositories.Interfaces;

public interface IEntity<T>
{
    T Id { get; set; }
}