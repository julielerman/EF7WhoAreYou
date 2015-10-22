using EF7Samurai.Domain;
using Microsoft.Data.Entity;

namespace EF7Samurai.Context
{

  public class SamuraiContext : DbContext
  {

    bool _useSqlServer;
    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Battle> Battles { get; set; }
    public DbSet<Quote> Quotes { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      if (_useSqlServer) {
        optionsBuilder
            .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database=EF7Samurai; Trusted_Connection=True; MultipleActiveResultSets = True;")
            .MaxBatchSize(40);
      }
      else
      {
        optionsBuilder.UseInMemoryDatabase(); }
  
    }
    public SamuraiContext() {

    }
    public SamuraiContext(bool useSqlServer) {
      _useSqlServer = useSqlServer;
    }
  }
}
