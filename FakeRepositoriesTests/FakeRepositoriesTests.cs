using System;
using System.Collections.Generic;
using System.Linq;
using FakeRepositories;
using FakeRepositories.Models;
using FakeRepositories.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeRepositoriesTests;

[TestClass]
public class Tests
{
    private List<Studio> _studioCollection; 
    private List<Series> _seriesCollection;
    private List<Genre> _genreCollection;
    private List<Character> _characterCollection;
    private List<Anime> _animeCollection;
    
    private FakeStudioRepository _studioRepository; 
    private FakeSeriesRepository _seriesRepository;
    private FakeGenreRepository _genreRepository;
    private FakeCharacterRepository _characterRepository;
    private FakeAnimeRepository _animeRepository;
    
    [TestInitialize]
    public void TestInitialize()
    { 
        _studioCollection = new List<Studio>();
        _seriesCollection = new List<Series>();
        _genreCollection = new List<Genre>();
        _characterCollection = new List<Character>();
        _animeCollection = new List<Anime>();
        
        _studioRepository = _studioCollection.CreateRepository();
        _genreRepository = _genreCollection.CreateRepository();
        _animeRepository = _animeCollection.CreateRepository(_genreCollection, _studioCollection);
        _seriesRepository = _seriesCollection.CreateRepository(_animeCollection);
        _characterRepository = _characterCollection.CreateRepository(_animeCollection);
    }
    
    [TestCleanup]
    public void TestCleanup()
    {
        _studioCollection.Clear();
        _seriesCollection.Clear();
        _genreCollection.Clear();
        _characterCollection.Clear();
        _animeCollection.Clear();
        
        _studioRepository = null;
        _genreRepository = null;
        _animeRepository = null;
        _seriesRepository = null;
        _characterRepository = null;
    }

    [TestMethod]
    public void FakeStudiosRepository_Add_AddStudiosInCollection()
    {
        // Arrange
        var studio1 = new Studio("studio1");
        var studio2 = new Studio("studio2");

        // Act
        _studioRepository.Add(studio1);
        _studioRepository.Add(studio2);
        
        // Assert
        Assert.IsTrue(_studioCollection.Count == 2);
        Assert.IsTrue(_studioCollection[0] == studio1);
        Assert.IsTrue(_studioCollection[1] == studio2);
    }
    
    [TestMethod]
    public void FakeStudioRepository_Remove_RemoveStudiosInCollection()
    {
        // Arrange
        PrepareStudioRepository(out var studio1, out var studio2);

        // Act
        _studioRepository.Delete(studio1);
        
        // Assert
        Assert.IsTrue(_studioCollection.Count == 1);
        Assert.IsTrue(_studioCollection[0] == studio2);
    }
    
    [TestMethod]
    public void FakeStudioRepository_GetByNotExistsId_ThrowsException()
    {
        // Arrange
        PrepareStudioRepository(out _, out _);

        // Act and Assert
        Assert.ThrowsException<InvalidOperationException>(() => _studioRepository.GetById(3));
    }
    
    [TestMethod]
    public void FakeStudioRepository_GetByExistsId_ReturnsStudio()
    {
        // Arrange
        PrepareStudioRepository(out _, out var studio2);
        var id = studio2.Id;

        // Act
        var studio = _studioRepository.GetById(id);
        
        // Assert
        Assert.IsTrue(studio.Id == id);
    }
    
    [TestMethod]
    public void FakeStudioRepository_GetCount_ReturnsCountOfStudios()
    {
        // Arrange
        PrepareStudioRepository(out _, out _);

        // Act
        var count = _studioRepository.GetCount();
        var expectedCount = 2;
        
        // Assert
        Assert.AreEqual(expectedCount, count);
    }
    
    [TestMethod]
    public void FakeStudioRepository_GetAll_ReturnsCollectionWithoutChanges()
    {
        // Arrange
        PrepareStudioRepository(out var studio1, out var studio2);

        // Act
        var collection = _studioRepository.GetAll().ToList();
        var expectedCollection = new List<Studio>{studio1, studio2};
        
        // Assert
        CollectionAssert.AreEqual(expectedCollection, collection);
    }

    [TestMethod]
    public void FakeGenreRepository_Add_AddGenresInCollection()
    {
        // Arrange
        var genre1 = new Genre("Genre1");
        var genre2 = new Genre("Genre2");

        // Act
        _genreRepository.Add(genre1);
        _genreRepository.Add(genre2);
        
        // Assert
        Assert.IsTrue(_genreCollection.Count == 2);
        Assert.IsTrue(_genreCollection[0] == genre1);
        Assert.IsTrue(_genreCollection[1] == genre2);
    }
    
    [TestMethod]
    public void FakeGenreRepository_Remove_RemoveGenreInCollection()
    {
        // Arrange
        PrepareGenreRepository(out var genre1, out var genre2);

        // Act
        _genreRepository.Delete(genre1);
        
        // Assert
        Assert.IsTrue(_genreCollection.Count == 1);
        Assert.IsTrue(_genreCollection[0] == genre2);
    }
    
    [TestMethod]
    public void FakeGenreRepository_GetByNotExistsId_ThrowsException()
    {
        // Arrange
        PrepareGenreRepository(out _, out _);

        // Act and Assert
        Assert.ThrowsException<InvalidOperationException>(() => _studioRepository.GetById(3));
    }
    
    [TestMethod]
    public void FakeGenreRepository_GetByExistsId_ReturnsGenre()
    {
        // Arrange
        PrepareGenreRepository(out _, out var genre2);
        var id = genre2.Id;

        // Act
        var genre = _genreRepository.GetById(id);
        
        // Assert
        Assert.IsTrue(genre.Id == id);
    }
    
    [TestMethod]
    public void FakeGenreRepository_GetCount_ReturnsCountOfGenres()
    {
        // Arrange
        PrepareGenreRepository(out _, out _);

        // Act
        var count = _genreRepository.GetCount();
        var expectedCount = 2;
        
        // Assert
        Assert.AreEqual(expectedCount, count);
    }
    
    [TestMethod]
    public void FakeGenreRepository_GetAll_ReturnsCollectionWithoutChanges()
    {
        // Arrange
        PrepareGenreRepository(out var genre1, out var genre2);

        // Act
        var collection = _genreRepository.GetAll().ToList();
        var expectedCollection = new List<Genre>{genre1, genre2};
        
        // Assert
        CollectionAssert.AreEqual(expectedCollection, collection);
    }
    
    [TestMethod]
    public void FakeAnimeRepository_Add_AddAnimeInCollection()
    {
        // Arrange
        PrepareGenreRepository(out _, out _);
        PrepareStudioRepository(out _, out _);
        
        var anime1 = new Anime("Anime1", true, "empty", "16+", "link",
            new List<int> { 1 }, new List<int> { 1 });
        var anime2 = new Anime("Anime2", true, "empty", "16+", "link",
            new List<int> { 2 }, new List<int> { 2 });

        // Act
        _animeRepository.Add(anime1);
        _animeRepository.Add(anime2);
        
        // Assert
        Assert.IsTrue(_animeCollection.Count == 2);
        Assert.IsTrue(_animeCollection[0] == anime1);
        Assert.IsTrue(_animeCollection[1] == anime2);
    }
    
    [TestMethod]
    public void FakeAnimeRepository_Remove_RemoveAnimeInCollection()
    {
        // Arrange
        PrepareAnimeRepository(out var anime1, out var anime2);

        // Act
        _animeRepository.Delete(anime1);
        
        // Assert
        Assert.IsTrue(_animeCollection.Count == 1);
        Assert.IsTrue(_animeCollection[0] == anime2);
    }
    
    [TestMethod]
    public void FakeAnimeRepository_GetByNotExistsId_ThrowsException()
    {
        // Arrange
        PrepareAnimeRepository(out _, out _);

        // Act and Assert
        Assert.ThrowsException<InvalidOperationException>(() => _studioRepository.GetById(3));
    }
    
    [TestMethod]
    public void FakeAnimeRepository_GetByExistsId_ReturnsAnime()
    {
        // Arrange
        PrepareAnimeRepository(out _, out var anime2);
        var id = anime2.Id;

        // Act
        var genre = _animeRepository.GetById(id);
        
        // Assert
        Assert.IsTrue(genre.Id == id);
    }
    
    [TestMethod]
    public void FakeAnimeRepository_GetCount_ReturnsCountOfAnime()
    {
        // Arrange
        PrepareAnimeRepository(out _, out _);

        // Act
        var count = _animeRepository.GetCount();
        var expectedCount = 2;
        
        // Assert
        Assert.AreEqual(expectedCount, count);
    }
    
    [TestMethod]
    public void FakeAnimeRepository_GetAll_ReturnsCollectionWithoutChanges()
    {
        // Arrange
        PrepareAnimeRepository(out var anime1, out var anime2);

        // Act
        var collection = _animeRepository.GetAll().ToList();
        var expectedCollection = new List<Anime>{anime1, anime2};
        
        // Assert
        CollectionAssert.AreEqual(expectedCollection, collection);
    }

    [TestMethod]
    public void FakeSeriesRepository_Add_AddSeriesInCollection()
    {
        // Arrange
        PrepareAnimeRepository(out _, out _);
        
        var series1 = new Series(1, 1, 1, "20 минут", "link");
        var series2 = new Series(2, 1, 1, "20 минут", "link");

        // Act
        _seriesRepository.Add(series1);
        _seriesRepository.Add(series2);
        
        // Assert
        Assert.IsTrue(_seriesCollection.Count == 2);
        Assert.IsTrue(_seriesCollection[0] == series1);
        Assert.IsTrue(_seriesCollection[1] == series2);
    }
    
    [TestMethod]
    public void FakeSeriesRepository_Remove_RemoveSeriesInCollection()
    {
        // Arrange
        PrepareSeriesRepository(out var series1, out var series2);

        // Act
        _seriesRepository.Delete(series1);
        
        // Assert
        Assert.IsTrue(_seriesCollection.Count == 1);
        Assert.IsTrue(_seriesCollection[0] == series2);
    }
    
    [TestMethod]
    public void FakeSeriesRepository_GetByNotExistsId_ThrowsException()
    {
        // Arrange
        PrepareSeriesRepository(out _, out _);

        // Act and Assert
        Assert.ThrowsException<InvalidOperationException>(() => _studioRepository.GetById(3));
    }
    
    [TestMethod]
    public void FakeSeriesRepository_GetByExistsId_ReturnsSeries()
    {
        // Arrange
        PrepareSeriesRepository(out _, out var series2);
        var id = series2.Id;

        // Act
        var series = _seriesRepository.GetById(id);
        
        // Assert
        Assert.IsTrue(series.Id == id);
    }
    
    [TestMethod]
    public void FakeSeriesRepository_GetCount_ReturnsCountOfSeries()
    {
        // Arrange
        PrepareSeriesRepository(out _, out _);

        // Act
        var count = _seriesRepository.GetCount();
        var expectedCount = 2;
        
        // Assert
        Assert.AreEqual(expectedCount, count);
    }
    
    [TestMethod]
    public void FakeSeriesRepository_GetAll_ReturnsCollectionWithoutChanges()
    {
        // Arrange
        PrepareSeriesRepository(out var series1, out var series2);

        // Act
        var collection = _seriesRepository.GetAll().ToList();
        var expectedCollection = new List<Series>{series1, series2};
        
        // Assert
        CollectionAssert.AreEqual(expectedCollection, collection);
    }
    
    [TestMethod]
    public void FakeCharacterRepository_Add_AddCharacterInCollection()
    {
        // Arrange
        PrepareAnimeRepository(out _, out _);
        
        var character1 = new Character(1, "Name", "photo", true);
        var character2 = new Character(2, "Name", "photo", true);

        // Act
        _characterRepository.Add(character1);
        _characterRepository.Add(character2);
        
        // Assert
        Assert.IsTrue(_characterCollection.Count == 2);
        Assert.IsTrue(_characterCollection[0] == character1);
        Assert.IsTrue(_characterCollection[1] == character2);
    }
    
    [TestMethod]
    public void FakeCharacterRepository_Remove_RemoveCharacterInCollection()
    {
        // Arrange
        PrepareCharacterRepository(out var character1, out var character2);

        // Act
        _characterRepository.Delete(character1);
        
        // Assert
        Assert.IsTrue(_characterCollection.Count == 1);
        Assert.IsTrue(_characterCollection[0] == character2);
    }
    
    [TestMethod]
    public void FakeCharacterRepository_GetByNotExistsId_ThrowsException()
    {
        // Arrange
        PrepareCharacterRepository(out _, out _);

        // Act and Assert
        Assert.ThrowsException<InvalidOperationException>(() => _studioRepository.GetById(3));
    }
    
    [TestMethod]
    public void FakeCharacterRepository_GetByExistsId_ReturnsCharacter()
    {
        // Arrange
        PrepareCharacterRepository(out _, out var character2);
        var id = character2.Id;

        // Act
        var character = _characterRepository.GetById(id);
        
        // Assert
        Assert.IsTrue(character.Id == id);
    }
    
    [TestMethod]
    public void FakeCharacterRepository_GetCount_ReturnsCountOfCharacters()
    {
        // Arrange
        PrepareCharacterRepository(out _, out _);

        // Act
        var count = _characterRepository.GetCount();
        var expectedCount = 2;
        
        // Assert
        Assert.AreEqual(expectedCount, count);
    }
    
    [TestMethod]
    public void FakeCharacterRepository_GetAll_ReturnsCollectionWithoutChanges()
    {
        // Arrange
        PrepareCharacterRepository(out var character1, out var character2);

        // Act
        var collection = _characterRepository.GetAll().ToList();
        var expectedCollection = new List<Character>{character1, character2};
        
        // Assert
        CollectionAssert.AreEqual(expectedCollection, collection);
    }

    private void PrepareStudioRepository(out Studio studio1, out Studio studio2)
    { 
        studio1 = new Studio("studio1");
        studio2 = new Studio("studio2");
        
        _studioRepository.Add(studio1);
        _studioRepository.Add(studio2);
    }
    
    private void PrepareGenreRepository(out Genre genre1, out Genre genre2)
    { 
        genre1 = new Genre("Genre1");
        genre2 = new Genre("Genre2");
        
        _genreRepository.Add(genre1);
        _genreRepository.Add(genre2);
    }
    
    private void PrepareAnimeRepository(out Anime anime1, out Anime anime2)
    { 
        anime1 = new Anime("Anime1", true, "empty", "16+", "link",
            new List<int> { 1 }, new List<int> { 1 });
        anime2 = new Anime("Anime2", true, "empty", "16+", "link",
            new List<int> { 2 }, new List<int> { 2 });

        PrepareStudioRepository(out _, out _);
        PrepareGenreRepository(out _, out _);
        
        _animeRepository.Add(anime1);
        _animeRepository.Add(anime2);
    }
    
    private void PrepareSeriesRepository(out Series series1, out Series series2)
    { 
        PrepareAnimeRepository(out _, out _);
        
        series1 = new Series(1, 1, 1, "20 минут", "link");
        series2 = new Series(2, 1, 1, "20 минут", "link");

        _seriesRepository.Add(series1);
        _seriesRepository.Add(series2);
    } 
    
    private void PrepareCharacterRepository(out Character character1, out Character character2)
    { 
        PrepareAnimeRepository(out _, out _);
        
        character1 = new Character(1, "Name", "photo", true);
        character2 = new Character(2, "Name", "photo", true);

        _characterRepository.Add(character1);
        _characterRepository.Add(character2);
    } 
}