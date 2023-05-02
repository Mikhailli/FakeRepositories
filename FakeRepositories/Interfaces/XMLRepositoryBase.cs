using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using FakeRepositories.Models;

namespace FakeRepositories.Interfaces;

public class XmlRepositoryBase : IRepository<Character>
{
    private XmlDocument _xDoc = new XmlDocument();

    private string _filename =
        "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\FakeRepositories\\xmls\\Characters.xml";
    
    public XmlRepositoryBase()
    {
        _xDoc.Load(_filename);
    }
    
    public IEnumerable<Character> GetAll()
    {
        return ConvertXMLToModel.ConvertXMLToCharacter();
    }

    public void Insert(Character character, bool autoPersist = true)
    {
        var characterElement = _xDoc.CreateElement("character");
        
        var nameAttr = _xDoc.CreateAttribute("name");
        var id = _xDoc.CreateAttribute("id");
        var photo = _xDoc.CreateElement("photo");
        var isMainCharacter = _xDoc.CreateElement("isMainCharacter");
        
        var nameText = _xDoc.CreateTextNode(character.Name);
        var nameId = _xDoc.CreateTextNode(character.Id.ToString());
        var photoText = _xDoc.CreateTextNode(character.Photo);
        var isMainCharacterText = _xDoc.CreateTextNode(character.IsMainCharacter.ToString());
        
        nameAttr.AppendChild(nameText);
        id.AppendChild(nameId);
        photo.AppendChild(photoText);
        isMainCharacter.AppendChild(isMainCharacterText);
        
        characterElement.Attributes.Append(nameAttr);
        characterElement.Attributes.Append(id);
        characterElement.AppendChild(photo);
        characterElement.AppendChild(isMainCharacter);
        
        var xRoot = _xDoc.DocumentElement;
        xRoot?.AppendChild(characterElement);
        Save();
    }

    public void Update(Character entidade, bool autoPersist = true)
    {
        throw new NotImplementedException();
    }

    public void Delete(Character entidade, bool autoPersist = true)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        _xDoc.Save(_filename);
    }
}