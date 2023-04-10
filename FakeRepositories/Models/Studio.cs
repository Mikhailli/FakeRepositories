using System.Collections.Generic;
using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Studio : Entity<int>
{
    public string Title { get; }

    public Studio(string title) => Title = title;
}