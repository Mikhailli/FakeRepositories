using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using FakeRepositories.Models;

namespace FakeRepositories.Interfaces;

public class XmlAnimeRepository : IRepository<Anime>
{
    private XmlDocument _xDoc = new ();
    private XmlGenreRepository _genreRepository;
    private XmlStudioRepository _studioRepository; 

    private string _filename =
        "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\FakeRepositories\\xmls\\Animes.xml";
    
    public XmlAnimeRepository(XmlGenreRepository genreRepository = null, XmlStudioRepository studioRepository = null)
    {
        _xDoc.Load(_filename);
        if (genreRepository is null && studioRepository is null)
        {
            _genreRepository = new XmlGenreRepository(this);
            _studioRepository = new XmlStudioRepository(this);
        }
        else if (genreRepository is not null && studioRepository is null)
        {
            _genreRepository = genreRepository;
            _studioRepository = new XmlStudioRepository(this);
        }
        else
        {
            _genreRepository = new XmlGenreRepository(this);
            _studioRepository = studioRepository;
        }
    }
    
    public IEnumerable<Anime> GetAll()
    {
        return ConvertXMLToModel.ConvertXMLToAnimes();
    }

    public void Insert(Anime anime)
    {
        var animeElement = _xDoc.CreateElement("anime");
        
        var nameAttr = _xDoc.CreateAttribute("name");
        var id = _xDoc.CreateAttribute("id");
        var isEnd = _xDoc.CreateElement("isend");
        var description = _xDoc.CreateElement("description");
        var ageLimit = _xDoc.CreateElement("agelimit");
        var link = _xDoc.CreateElement("link");
        var studio_refs = _xDoc.CreateElement("studio_refs");
        var studioRefs = new List<XmlElement>();
        foreach (var _ in anime.StudiosIds)
        {
            studioRefs.Add(_xDoc.CreateElement("studio_ref"));
        }

        var studioRefsIds = new List<XmlAttribute>();
        for (var i = 0; i < studioRefs.Count; i++)
        {
            studioRefsIds.Add(_xDoc.CreateAttribute("id"));
        }
        
        var studioRefsIdsTexts = new List<XmlText>();
        for (var i = 0; i < studioRefs.Count; i++)
        {
            studioRefsIdsTexts.Add(_xDoc.CreateTextNode("studio" + anime.StudiosIds.ToList()[i]));
        }
        for (var i = 0; i < studioRefs.Count; i++)
        {
            studioRefsIds[i].AppendChild(studioRefsIdsTexts[i]);
        }
        for (var i = 0; i < studioRefs.Count; i++)
        {
            studioRefs[i].Attributes.Append(studioRefsIds[i]);
        }
        foreach (var studioRef in studioRefs)
        {
            studio_refs.AppendChild(studioRef);
        }
        
        var genre_refs = _xDoc.CreateElement("genre_refs");
        var genreRefs = new List<XmlElement>();
        foreach (var _ in anime.GenresIds)
        {
            genreRefs.Add(_xDoc.CreateElement("genre_ref"));
        }

        var genreRefsIds = new List<XmlAttribute>();
        for (var i = 0; i < genreRefs.Count; i++)
        {
            genreRefsIds.Add(_xDoc.CreateAttribute("id"));
        }
        
        var genreRefsIdsTexts = new List<XmlText>();
        for (var i = 0; i < genreRefs.Count; i++)
        {
            genreRefsIdsTexts.Add(_xDoc.CreateTextNode("genre" + anime.GenresIds.ToList()[i]));
        }
        for (var i = 0; i < genreRefs.Count; i++)
        {
            genreRefsIds[i].AppendChild(genreRefsIdsTexts[i]);
        }
        for (var i = 0; i < genreRefs.Count; i++)
        {
            genreRefs[i].Attributes.Append(genreRefsIds[i]);
        }
        foreach (var genreRef in genreRefs)
        {
            genre_refs.AppendChild(genreRef);
        }
        
        var nameText = _xDoc.CreateTextNode(anime.Title);
        var nameId = _xDoc.CreateTextNode("anime" + anime.Id);
        var isEndText = _xDoc.CreateTextNode(anime.IsEnd.ToString().ToLower());
        var descriptionText = _xDoc.CreateTextNode(anime.Description);
        var ageLimitText = _xDoc.CreateTextNode(anime.AgeLimit);
        var linkText = _xDoc.CreateTextNode(anime.Link);

        nameAttr.AppendChild(nameText);
        id.AppendChild(nameId);
        isEnd.AppendChild(isEndText);
        description.AppendChild(descriptionText);
        ageLimit.AppendChild(ageLimitText);
        link.AppendChild(linkText);

        animeElement.Attributes.Append(nameAttr);
        animeElement.Attributes.Append(id);
        animeElement.AppendChild(isEnd);
        animeElement.AppendChild(description);
        animeElement.AppendChild(ageLimit);
        animeElement.AppendChild(link);
        animeElement.AppendChild(studio_refs);
        animeElement.AppendChild(genre_refs);

        var xRoot = _xDoc.DocumentElement;
        xRoot?.AppendChild(animeElement);
        Save();

        foreach (var genre in GetGenresByAnime(anime))
        {
            if (genre.AnimeIds.Contains(anime.Id) is false)
            {
                genre.AnimeIds.Add(anime.Id);
                _genreRepository.Update(genre);
            }
        }
        
        foreach (var studio in GetStudiosByAnime(anime))
        {
            if (studio.AnimeIds.Contains(anime.Id) is false)
            {
                studio.AnimeIds.Add(anime.Id);
                _studioRepository.Update(studio);
            }
        }
    }

    public void Update(Anime anime)
    {
        var xRoot = _xDoc.DocumentElement;
        if (xRoot is not null)
        {
            foreach (XmlElement xNode in xRoot)
            {
                var id = Convert.ToInt32(xNode.Attributes.GetNamedItem("id").Value.Replace("anime", ""));
                if (id == anime.Id)
                {
                    Delete(id);
                    Insert(anime);
                }
            }
        }
    }

    public void Delete(int idToDelete)
    {
        var xRoot = _xDoc.DocumentElement;
        if (xRoot is not null)
        {
            foreach (XmlElement xNode in xRoot)
            {
                var id = Convert.ToInt32(xNode.Attributes.GetNamedItem("id").Value.Replace("anime", ""));
                if (id == idToDelete)
                {
                    xNode.RemoveAll();
                    xRoot.RemoveChild(xNode);
                    Save();
                }
            }
        }
    }

    public void Save()
    {
        _xDoc.Save(_filename);
    }

    public void Update()
    {
        _xDoc.Load(_filename);
    }
    
    private List<Genre> GetGenresByAnime(Anime anime)
    {
        _genreRepository.Update();
        var allGenres = _genreRepository.GetAll();
        return allGenres.Where(genre => anime.GenresIds.Contains(genre.Id)).ToList();
    }
    
    private List<Studio> GetStudiosByAnime(Anime anime)
    {
        _studioRepository.Update();
        var allStudios = _studioRepository.GetAll();
        return allStudios.Where(studio => anime.GenresIds.Contains(studio.Id)).ToList();
    }
}