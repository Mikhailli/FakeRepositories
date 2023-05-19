using System.Xml;
using FakeRepositories.Interfaces;
using WebForFakeRepositories.Models;

namespace WebForFakeRepositories.Interfaces;

public class XmlStudioRepository : IRepository<Studio>
{
    private readonly XmlDocument _xDoc = new ();
    private readonly XmlAnimeRepository _animeRepository;

    private const string Filename = "C:\\Users\\Михаил\\RiderProjects\\FakeRepositories\\WebForFakeRepositories\\xmls\\Studios.xml";

    public XmlStudioRepository(XmlAnimeRepository repository = null)
    {
        _xDoc.Load(Filename);
        _animeRepository = repository ?? new XmlAnimeRepository(null, this);
    }
    
    public IEnumerable<Studio> GetAll()
    {
        return ConvertXMLToModel.ConvertXMLToStudio();
    }

    public void Insert(Studio studio)
    {
        var studioElement = _xDoc.CreateElement("studio");
        
        var nameAttr = _xDoc.CreateAttribute("name");
        var id = _xDoc.CreateAttribute("id");
        var animeRefsElement = _xDoc.CreateElement("anime_refs");
        var animeRefs = studio.AnimeIds.Select(_ => _xDoc.CreateElement("anime_ref")).ToList();

        var animeRefsIds = new List<XmlAttribute>();
        for (var i = 0; i < animeRefs.Count; i++)
        {
            animeRefsIds.Add(_xDoc.CreateAttribute("id"));
        }
        
        var animeRefsIdsTexts = new List<XmlText>();
        for (var i = 0; i < animeRefs.Count; i++)
        {
            animeRefsIdsTexts.Add(_xDoc.CreateTextNode("anime" + studio.AnimeIds.ToList()[i]));
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
            animeRefsElement.AppendChild(animeRef);
        }
        
        var nameText = _xDoc.CreateTextNode(studio.Title);
        var nameId = _xDoc.CreateTextNode("studio" + studio.Id);

        nameAttr.AppendChild(nameText);
        id.AppendChild(nameId);

        studioElement.Attributes.Append(nameAttr);
        studioElement.Attributes.Append(id);
        studioElement.AppendChild(animeRefsElement);

        var xRoot = _xDoc.DocumentElement;
        xRoot?.AppendChild(studioElement);
        Save();
        
        foreach (var anime in GetAnimesByStudio(studio).Where(anime => anime.GenresIds.Contains(studio.Id) is false))
        {
            anime.GenresIds.Add(anime.Id);
            _animeRepository.Update(anime);
        }
    }

    public void Update(Studio studio)
    {
        var xRoot = _xDoc.DocumentElement;
        if (xRoot is not null)
        {
            foreach (var id in from XmlElement xNode in xRoot select Convert.ToInt32(xNode.Attributes.GetNamedItem("id").Value.Replace("studio", "")) into id where id == studio.Id select id)
            {
                Delete(id);
                Insert(studio);
            }
        }
    }

    public void Delete(int idToDelete)
    {
        var xRoot = _xDoc.DocumentElement;
        if (xRoot is not null)
        {
            foreach (var xNode in from XmlElement xNode in xRoot let id = Convert.ToInt32(xNode.Attributes.GetNamedItem("id").Value.Replace("studio", "")) where id == idToDelete select xNode)
            {
                xNode.RemoveAll();
                xRoot.RemoveChild(xNode);
                Save();
            }
        }
    }

    public void Save()
    {
        _xDoc.Save(Filename);
    }
    
    public void Update()
    {
        _xDoc.Load(Filename);
    }
    
    private List<Anime> GetAnimesByStudio(Studio studio)
    {
        _animeRepository.Update();
        var allAnimes = _animeRepository.GetAll();
        return allAnimes.Where(anime => studio.AnimeIds.Contains(anime.Id)).ToList();
    }
}
