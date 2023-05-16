using System;
using System.Collections.Generic;
using System.Xml;
using FakeRepositories.Models;

namespace FakeRepositories.Interfaces;

public class XmlSeriesRepository : IRepository<Series>
{
    private XmlDocument _xDoc = new ();

    private readonly string _filename =
        "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\FakeRepositories\\xmls\\Series.xml";
    
    public XmlSeriesRepository()
    {
        _xDoc.Load(_filename);
    }
    
    public IEnumerable<Series> GetAll()
    {
        return ConvertXMLToModel.ConvertXMLToSeries();
    }

    public void Insert(Series studio)
    {
        var seriesElement = _xDoc.CreateElement("serie");
        
        
        var id = _xDoc.CreateAttribute("id");
        var season = _xDoc.CreateElement("season");
        var number = _xDoc.CreateElement("number");
        var duration = _xDoc.CreateElement("duration");
        var link = _xDoc.CreateElement("link");
        var anime = _xDoc.CreateElement("anime");
        var animeId = _xDoc.CreateAttribute("id");
        
        var nameId = _xDoc.CreateTextNode("serie" + studio.Id);
        var seasonText = _xDoc.CreateTextNode(studio.SeasonNumber.ToString());
        var numberText = _xDoc.CreateTextNode(studio.SeasonNumber.ToString());
        var durationText = _xDoc.CreateTextNode(studio.SeriesDuration.ToString());
        var linkText = _xDoc.CreateTextNode(studio.Link);
        var animeIdText = _xDoc.CreateTextNode("anime" + studio.AnimeId);
        
        id.AppendChild(nameId);
        season.AppendChild(seasonText);
        number.AppendChild(numberText);
        duration.AppendChild(durationText);
        link.AppendChild(linkText);
        animeId.AppendChild(animeIdText);
        anime.Attributes.Append(animeId);
        
        seriesElement.Attributes.Append(id);
        seriesElement.AppendChild(season);
        seriesElement.AppendChild(number);
        seriesElement.AppendChild(duration);
        seriesElement.AppendChild(link);
        seriesElement.AppendChild(anime);

        var xRoot = _xDoc.DocumentElement;
        xRoot?.AppendChild(seriesElement);
        Save();
    }

    public void Update(Series studio)
    {
        var xRoot = _xDoc.DocumentElement;
        if (xRoot is not null)
        {
            foreach (XmlElement xNode in xRoot)
            {
                var id = Convert.ToInt32(xNode.Attributes.GetNamedItem("id").Value.Replace("serie", ""));
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
                var id = Convert.ToInt32(xNode.Attributes.GetNamedItem("id").Value.Replace("serie", ""));
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
}