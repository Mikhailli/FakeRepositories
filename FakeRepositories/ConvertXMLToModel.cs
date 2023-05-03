#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using FakeRepositories.Models;

namespace FakeRepositories;

public static class ConvertXMLToModel
{
    public static List<Genre> ConvertXMLToGenre()
    {
        var document = new XmlDocument();
        document.Load("C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\FakeRepositories\\xmls\\Genres.xml");
        
        var xRoot = document.DocumentElement;
        var genreNodes = xRoot?.SelectNodes("genre");

        var titles = new List<string>();
        var ids = new List<int>();
        var animeIds = new List<List<int>>();

        if (genreNodes is not null)
        {
            foreach (var node in genreNodes)
            {
                var refNodes = (node as XmlNode)?.ChildNodes.Item(0)?.ChildNodes;
                if (refNodes is not null)
                {
                    var tempList = new List<int>();
                    foreach (XmlNode refNode in refNodes)
                    {
                        if (refNode.Attributes is not null)
                        {
                            tempList.Add(Convert.ToInt32(refNode.Attributes[0]?.Value.Replace("anime", "")));
                        }
                    }

                    animeIds.Add(tempList);
                }
            }
            
            titles.AddRange(from XmlNode node in genreNodes select node.SelectSingleNode("@name")?.Value);
            ids.AddRange(from XmlNode node in genreNodes
                select Convert.ToInt32(node.SelectSingleNode("@id")?.Value.Replace("genre", "")));
        }

        var genres = new List<Genre>();
        for (var i = 0; i < ids.Count; i++)
        {
            var genre = new Genre(titles[i], animeIds[i])
            {
                Id = ids[i]
            };
            genres.Add(genre);
        }

        return genres;
    }
    
    public static List<Studio> ConvertXMLToStudio()
    {
        var document = new XmlDocument();
        document.Load("C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\FakeRepositories\\xmls\\Studios.xml");
        
        var xRoot = document.DocumentElement;
        var studioNodes = xRoot?.SelectNodes("studio");

        var titles = new List<string>();
        var ids = new List<int>();
        var animeIds = new List<List<int>>();

        if (studioNodes is not null)
        {
            foreach (var node in studioNodes)
            {
                var refNodes = (node as XmlNode)?.ChildNodes.Item(0)?.ChildNodes;
                if (refNodes is not null)
                {
                    var tempList = new List<int>();
                    foreach (XmlNode refNode in refNodes)
                    {
                        if (refNode.Attributes is not null)
                        {
                            tempList.Add(Convert.ToInt32(refNode.Attributes[0]?.Value.Replace("anime", "")));
                        }
                    }

                    animeIds.Add(tempList);
                }
            }
            
            titles.AddRange(from XmlNode node in studioNodes select node.SelectSingleNode("@name")?.Value);
            ids.AddRange(from XmlNode node in studioNodes
                select Convert.ToInt32(node.SelectSingleNode("@id")?.Value.Replace("studio", "")));
        }

        var studios = new List<Studio>();
        for (var i = 0; i < ids.Count; i++)
        {
            var studio = new Studio(titles[i], animeIds[i])
            {
                Id = ids[i]
            };
            studios.Add(studio);
        }

        return studios;
    }
    
    public static List<Character> ConvertXMLToCharacter()
    {
        var document = new XmlDocument();
        document.Load("C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\FakeRepositories\\xmls\\Characters.xml");
        
        var xRoot = document.DocumentElement;
        var characterNodes = xRoot?.SelectNodes("character");

        var titles = new List<string>();
        var ids = new List<int>();
        var animeIds = new List<int>();
        var photos = new List<string>();
        var isMainCharacter = new List<string>();

        if (characterNodes is not null)
        {
            animeIds.AddRange(from XmlNode node in characterNodes select Convert.ToInt32(node.SelectSingleNode("anime")?.Attributes[0].Value.Replace("anime", "")));
            titles.AddRange(from XmlNode node in characterNodes select node.SelectSingleNode("@name")?.Value);
            photos.AddRange(from XmlNode node in characterNodes select node.SelectSingleNode("photo")?.InnerText);
            isMainCharacter.AddRange(from XmlNode node in characterNodes
                select node.SelectSingleNode("ismaincharacter")?.InnerText);
            ids.AddRange(from XmlNode node in characterNodes
                select Convert.ToInt32(node.SelectSingleNode("@id")?.Value.Replace("character", "")));
        }

        var characters = new List<Character>();
        for (var i = 0; i < ids.Count; i++)
        {
            var character = new Character(titles[i], photos[i], isMainCharacter[i] == "true")
            {
                Id = ids[i],
                AnimeId = animeIds[i]
            };
            characters.Add(character);
        }

        return characters;
    }
    
    public static List<Series> ConvertXMLToSeries()
    {
        var document = new XmlDocument();
        document.Load("C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\FakeRepositories\\xmls\\Series.xml");
        
        var xRoot = document.DocumentElement;
        var seriesNodes = xRoot?.SelectNodes("serie");

        var links = new List<string>();
        var ids = new List<int>();
        var animeIds = new List<int>();
        var seasons = new List<int>();
        var numbers = new List<int>();
        var durations = new List<int>();
        if (seriesNodes is not null)
        {
            links.AddRange(from XmlNode node in seriesNodes select node.SelectSingleNode("link")?.InnerText);
            ids.AddRange(from XmlNode node in seriesNodes select Convert.ToInt32(node.SelectSingleNode("@id")?.Value.Replace("serie", "")));
            seasons.AddRange(from XmlNode node in seriesNodes select Convert.ToInt32(node.SelectSingleNode("season")?.InnerText));
            animeIds.AddRange(from XmlNode node in seriesNodes select Convert.ToInt32(node.SelectSingleNode("anime")?.Attributes[0].Value.Replace("anime", "")));
            numbers.AddRange(from XmlNode node in seriesNodes select Convert.ToInt32(node.SelectSingleNode("number")?.InnerText));
            durations.AddRange(from XmlNode node in seriesNodes select Convert.ToInt32(node.SelectSingleNode("duration")?.InnerText));
        }

        var series = new List<Series>();
        for (var i = 0; i < ids.Count; i++)
        {
            var serie = new Series(numbers[i], seasons[i], durations[i], links[i])
            {
                Id = ids[i],
                AnimeId = animeIds[i]
            };
            series.Add(serie);
        }

        return series;
    }
    
    public static List<Anime> ConvertXMLToAnimes()
    {
        var document = new XmlDocument();
        document.Load("C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\FakeRepositories\\xmls\\Animes.xml");
        
        var xRoot = document.DocumentElement;
        var animeNodes = xRoot?.SelectNodes("anime");

        var titles = new List<string>();
        var isEnds = new List<string>();
        var descriptions = new List<string>();
        var ageLimits = new List<string>();
        var links = new List<string>();
        var ids = new List<int>();
        var studioIds = new List<List<int>>();
        var genreIds = new List<List<int>>();
        if (animeNodes is not null)
        {
            foreach (var node in animeNodes)
            {
                var refNodes = (node as XmlNode)?.ChildNodes.Item(4)?.ChildNodes;
                if (refNodes is not null)
                {
                    var tempList = new List<int>();
                    foreach (XmlNode refNode in refNodes)
                    {
                        if (refNode.Attributes is not null)
                        {
                            tempList.Add(Convert.ToInt32(refNode.Attributes[0]?.Value.Replace("studio", "")));
                        }
                    }
                    studioIds.Add(tempList);
                }
            }
            foreach (var node in animeNodes)
            {
                var refNodes = (node as XmlNode)?.ChildNodes.Item(5)?.ChildNodes;
                if (refNodes is not null)
                {
                    var tempList = new List<int>();
                    foreach (XmlNode refNode in refNodes)
                    {
                        if (refNode.Attributes is not null)
                        {
                            tempList.Add(Convert.ToInt32(refNode.Attributes[0]?.Value.Replace("genre", "")));
                        }
                    }
                    genreIds.Add(tempList);
                }
            }
            
            titles.AddRange(from XmlNode node in animeNodes select node.SelectSingleNode("@name")?.Value);
            ids.AddRange(from XmlNode node in animeNodes select Convert.ToInt32(node.SelectSingleNode("@id")?.Value.Replace("anime", "")));
            isEnds.AddRange(from XmlNode node in animeNodes select node.SelectSingleNode("isend")?.InnerText);
            descriptions.AddRange(from XmlNode node in animeNodes select node.SelectSingleNode("description")?.InnerText);
            ageLimits.AddRange(from XmlNode node in animeNodes select node.SelectSingleNode("agelimit")?.InnerText);
            links.AddRange(from XmlNode node in animeNodes select node.SelectSingleNode("link")?.InnerText);
        }

        var animes = new List<Anime>();
        for (var i = 0; i < ids.Count; i++)
        {
            var anime = new Anime(titles[i], isEnds[i] == "true", descriptions[i], ageLimits[i],
                links[i], studioIds[i], genreIds[i])
            {
                Id = ids[i]
            };
            animes.Add(anime);
        }

        return animes;
    }
}