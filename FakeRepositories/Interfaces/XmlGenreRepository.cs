using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using FakeRepositories.Models;

namespace FakeRepositories.Interfaces;

public class XmlGenreRepository : IRepository<Genre>
{
    private XmlDocument _xDoc = new ();
    private XmlAnimeRepository _animeRepository;

    private string _filename =
        "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\FakeRepositories\\xmls\\Genres.xml";
    
    public XmlGenreRepository(XmlAnimeRepository repository = null)
    {
        _xDoc.Load(_filename);
        _animeRepository = repository ?? new XmlAnimeRepository(this, null);
    }
    
    public IEnumerable<Genre> GetAll()
    {
        return ConvertXMLToModel.ConvertXMLToGenre();
    }

    public void Insert(Genre genre)
    {
        var genreElement = _xDoc.CreateElement("genre");
        
        var nameAttr = _xDoc.CreateAttribute("name");
        var id = _xDoc.CreateAttribute("id");
        var anime_refs = _xDoc.CreateElement("anime_refs");
        var animeRefs = new List<XmlElement>();
        foreach (var animeId in genre.AnimeIds)
        {
            animeRefs.Add(_xDoc.CreateElement("anime_ref"));
        }

        var animeRefsIds = new List<XmlAttribute>();
        for (var i = 0; i < animeRefs.Count; i++)
        {
            animeRefsIds.Add(_xDoc.CreateAttribute("id"));
        }
        
        var animeRefsIdsTexts = new List<XmlText>();
        for (var i = 0; i < animeRefs.Count; i++)
        {
            animeRefsIdsTexts.Add(_xDoc.CreateTextNode("anime" + genre.AnimeIds.ToList()[i]));
        }
        for (var i = 0; i < animeRefs.Count; i++)
        {
            animeRefsIds[i].AppendChild(animeRefsIdsTexts[i]);
        }
        for (var i = 0; i < animeRefs.Count; i++)
        {
            animeRefs[i].Attributes.Append(animeRefsIds[i]);
        }
        foreach (var animeRef in animeRefs)
        {
            anime_refs.AppendChild(animeRef);
        }
        
        var nameText = _xDoc.CreateTextNode(genre.Title);
        var nameId = _xDoc.CreateTextNode("genre" + genre.Id);

        nameAttr.AppendChild(nameText);
        id.AppendChild(nameId);

        genreElement.Attributes.Append(nameAttr);
        genreElement.Attributes.Append(id);
        genreElement.AppendChild(anime_refs);

        var xRoot = _xDoc.DocumentElement;
        xRoot?.AppendChild(genreElement);
        Save();
        
        foreach (var anime in GetAnimesByGenre(genre))
        {
            if (anime.GenresIds.Contains(genre.Id) is false)
            {
                anime.GenresIds.Add(anime.Id);
                _animeRepository.Update(anime);
            }
        }
    }

    public void Update(Genre studio)
    {
        var xRoot = _xDoc.DocumentElement;
        if (xRoot is not null)
        {
            foreach (XmlElement xNode in xRoot)
            {
                var id = Convert.ToInt32(xNode.Attributes.GetNamedItem("id").Value.Replace("genre", ""));
                if (id == studio.Id)
                {
                    Delete(id);
                    Insert(studio);
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
                var id = Convert.ToInt32(xNode.Attributes.GetNamedItem("id").Value.Replace("genre", ""));
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
    
    private List<Anime> GetAnimesByGenre(Genre genre)
    {
        _animeRepository.Update();
        var allAnimes = _animeRepository.GetAll();
        return allAnimes.Where(anime => genre.AnimeIds.Contains(anime.Id)).ToList();
    }
}