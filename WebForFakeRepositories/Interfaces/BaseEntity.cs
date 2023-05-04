#nullable enable
using System.ComponentModel.DataAnnotations;
using FakeRepositories.Interfaces;

namespace WebForFakeRepositories.Interfaces;

public abstract class BaseEntity
{
    
}

public abstract class Entity<T> : BaseEntity, IEntity<T>
{
    [Key] public virtual T Id { get; set; } = default!;
}