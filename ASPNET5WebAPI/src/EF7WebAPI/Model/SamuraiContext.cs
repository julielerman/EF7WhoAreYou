using Microsoft.Data.Entity;
using EF7Samurai.Domain;

namespace ASPNET5WebAPI.Model
{
  public class SamuraiContext : DbContext
    {

      public DbSet<Samurai> Samurais { get; set; }
      public DbSet<Battle> Battles { get; set; }
      public DbSet<Quote> Quotes { get; set; }
      public DbSet<Maker> Makers { get; set; }



      //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      //  if (_useInMemory) {
      //    optionsBuilder.UseInMemoryDatabase();
      //    return;
      //  }

      //  optionsBuilder
      //     .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database=EF7Samurai; Trusted_Connection=True; MultipleActiveResultSets = True;")
      //     .MaxBatchSize(40);


      //}
      //public SamuraiContext() {

      //}
      //public SamuraiContext(bool useInMemory) {
      //  _useInMemory = useInMemory;
      //}

    }
  }


