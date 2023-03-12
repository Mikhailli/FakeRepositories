using System.Collections.Generic;
using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Studio : Entity<int>
{
    private string Title { get; set; }

    public Studio(string title)
    {
        Title = title;
    }
}