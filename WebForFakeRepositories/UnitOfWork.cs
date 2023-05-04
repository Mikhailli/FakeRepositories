using FakeRepositories;
using FakeRepositories.Interfaces;
using FakeRepositories.Models;
using WebForFakeRepositories.Models;

namespace WebForFakeRepositories;

public class UnitOfWork : IDisposable
{
    private readonly ApplicationContext _context = new ();
    
    private IRepository<Anime>? _animeRepository;
    private IRepository<Character>? _characterRepository;
    private IRepository<Genre>? _genreRepository;
    private IRepository<Series>? _seriesRepository;
    private IRepository<Studio>? _studioRepository;
    
    private bool _disposed = false;
    
    public IRepository<Anime> Animes => _animeRepository ??= new EFGenericRepository<Anime>(_context);
    public IRepository<Character> Characters => _characterRepository ??= new EFGenericRepository<Character>(_context);
    public IRepository<Genre> Genres => _genreRepository ??= new EFGenericRepository<Genre>(_context);
    public IRepository<Series> Series => _seriesRepository ??= new EFGenericRepository<Series>(_context);
    public IRepository<Studio> Studios => _studioRepository ??= new EFGenericRepository<Studio>(_context);
    
    public void Save()
    {
        _context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed is false)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
 
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}