using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using FakeRepositories.Models;

namespace FakeRepositories.Interfaces;

public class XmlCharacterRepository : IRepository<Character>
{
    private XmlDocument _xDoc = new XmlDocument();

    private string _filename =
        "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\FakeRepositories\\xmls\\Characters.xml";
    
    public XmlCharacterRepository()
    {
        _xDoc.Load(_filename);
    }
    
    public IEnumerable<Character> GetAll()
    {
        return ConvertXMLToModel.ConvertXMLToCharacter();
    }

    public void Insert(Character studio)
    {
        var characterElement = _xDoc.CreateElement("character");
        
        var nameAttr = _xDoc.CreateAttribute("name");
        var id = _xDoc.CreateAttribute("id");
        var photo = _xDoc.CreateElement("photo");
        var isMainCharacter = _xDoc.CreateElement("isMainCharacter");
        var anime = _xDoc.CreateElement("anime");
        var animeId = _xDoc.CreateAttribute("id");

        var nameText = _xDoc.CreateTextNode(studio.Name);
        var nameId = _xDoc.CreateTextNode(studio.Id.ToString());
        var photoText = _xDoc.CreateTextNode(studio.Photo);
        var isMainCharacterText = _xDoc.CreateTextNode(studio.IsMainCharacter.ToString());
        var animeIdText = _xDoc.CreateTextNode("anime" + studio.AnimeId);

        nameAttr.AppendChild(nameText);
        id.AppendChild(nameId);
        photo.AppendChild(photoText);
        isMainCharacter.AppendChild(isMainCharacterText);
        animeId.AppendChild(animeIdText);
        
        characterElement.Attributes.Append(nameAttr);
        characterElement.Attributes.Append(id);
        characterElement.AppendChild(photo);
        characterElement.AppendChild(isMainCharacter);
        anime.Attributes.Append(animeId);
        characterElement.AppendChild(anime);
        
        var xRoot = _xDoc.DocumentElement;
        xRoot?.AppendChild(characterElement);
        Save();
    }

    public void Update(Character studio)
    {
        var xRoot = _xDoc.DocumentElement;
        if (xRoot is not null)
        {
            foreach (XmlElement xNode in xRoot)
            {
                var id = Convert.ToInt32(xNode.Attributes.GetNamedItem("id").Value.Replace("character", ""));
                if (id == studio.Id)
                {
                    Delete(id);

                    var characterElement = _xDoc.CreateElement("character");
        
                    var nameAttr = _xDoc.CreateAttribute("name");
                    var id1 = _xDoc.CreateAttribute("id");
                    var photo = _xDoc.CreateElement("photo");
                    var isMainCharacter = _xDoc.CreateElement("isMainCharacter");
                    var anime = _xDoc.CreateElement("anime");
                    var animeId = _xDoc.CreateAttribute("id");

                    var nameText = _xDoc.CreateTextNode(studio.Name);
                    var nameId = _xDoc.CreateTextNode(studio.Id.ToString());
                    var photoText = _xDoc.CreateTextNode(studio.Photo);
                    var isMainCharacterText = _xDoc.CreateTextNode(studio.IsMainCharacter.ToString().ToLower());
                    var animeIdText = _xDoc.CreateTextNode("anime" + studio.AnimeId);

                    nameAttr.AppendChild(nameText);
                    id1.AppendChild(nameId);
                    photo.AppendChild(photoText);
                    isMainCharacter.AppendChild(isMainCharacterText);
                    animeId.AppendChild(animeIdText);
        
                    characterElement.Attributes.Append(nameAttr);
                    characterElement.Attributes.Append(id1);
                    characterElement.AppendChild(photo);
                    characterElement.AppendChild(isMainCharacter);
                    anime.Attributes.Append(animeId);
                    characterElement.AppendChild(anime);
                    
                    xRoot?.AppendChild(characterElement);
                    Save();
                }
            }
        }
        
        Save();
    }

    public void Delete(int idToDelete)
    {
        var xRoot = _xDoc.DocumentElement;
        if (xRoot is not null)
        {
            foreach (XmlElement xNode in xRoot)
            {
                var id = Convert.ToInt32(xNode.Attributes.GetNamedItem("id").Value.Replace("character", ""));
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