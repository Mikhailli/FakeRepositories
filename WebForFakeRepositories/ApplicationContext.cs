using System.Data.Entity;
using WebForFakeRepositories.Models;

namespace WebForFakeRepositories;

public class ApplicationContext : DbContext
{
    public DbSet<Anime> Anime { get; set; }
    public DbSet<Character> Character { get; set; }
    public DbSet<Genre> Genre { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Studio> Studio { get; set; }
    
    public ApplicationContext()
            : base("Server=(localdb)\\mssqllocaldb;Database=Lab№5;Trusted_Connection=True;")
    {
        
    }
    
}