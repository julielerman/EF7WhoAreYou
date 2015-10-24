﻿using EF7Samurai.Domain;
using Microsoft.Data.Entity;

namespace EF7Samurai.Context
{

  public class SamuraiContext : DbContext
  {

    bool _useInMemory;
    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Battle> Battles { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Maker> Makers { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      if ( _useInMemory) {
        optionsBuilder.UseInMemoryDatabase();
        return;
      }
      
        optionsBuilder
           .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database=EF7Samurai; Trusted_Connection=True; MultipleActiveResultSets = True;")
           .MaxBatchSize(40);
     
  
    }
    public SamuraiContext() {

    }
    public SamuraiContext(bool useInMemory) {
      _useInMemory = useInMemory;
    }
    //public SamuraiContext(DbContextOptionsBuilder options) : base() { }
  }
}
