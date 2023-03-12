﻿using System.Collections.Generic;
using FakeRepositories.Interfaces;

namespace FakeRepositories.Models;

public class Anime : Entity<int>
{
    public string Title { get; set; }

    public bool IsEnd { get; set; }

    public string Description { get; set; }

    public string AgeLimit { get; set; }

    public string Link { get; set; }

    public ICollection<int> StudioIds { get; set; }

    public ICollection<int> GenreIds { get; set; }

    public Anime(string title, bool isEnd, string description, string ageLimit, string link, 
        ICollection<int> studioIds, ICollection<int> genreIds)
    {
        Title = title;
        IsEnd = isEnd;
        Description = description;
        AgeLimit = ageLimit;
        Link = link;
        StudioIds = studioIds;
        GenreIds = genreIds;
    }
}