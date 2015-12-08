
using EF7Samurai.Domain;
using Microsoft.Data.Entity;

namespace EF7Samurai.Model
{
  public class SamuraiContext : DbContext
    {

      public DbSet<Samurai> Samurais { get; set; }
      public DbSet<Battle> Battles { get; set; }
      public DbSet<Quote> Quotes { get; set; }
      public DbSet<Maker> Makers { get; set; }


        }
  }


