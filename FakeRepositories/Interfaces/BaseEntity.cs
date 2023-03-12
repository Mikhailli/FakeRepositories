#nullable enable
using System.ComponentModel.DataAnnotations;

namespace FakeRepositories.Interfaces;

public abstract class BaseEntity
{
    
}

public abstract class Entity<T> : BaseEntity, IEntity<T>
{
    [Key] public virtual T Id { get; set; } = default!;
}