﻿using EF7Samurai.Domain;
using Microsoft.Data.Entity;

namespace EF7Samurai.B8.Context
{
  
  public class SamuraiContext : DbContext
  {
   
    bool _useSqlServer;
    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Battle> Battles { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder
            .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database=EF7Samurai; Trusted_Connection=True; MultipleActiveResultSets = True;")
            .MaxBatchSize(40);
        
        

     

    }

    public SamuraiContext() {

    }
    public SamuraiContext(bool UseSqlServer) : base() {
      _useSqlServer = UseSqlServer;

    }
#if false
        //alternate to hard coding options, pass them in from calling app
        //this would be the constructor
        //do not set options in OnConfiguring

        public SamuraiContext(DbContextOptions options) :base(options)
        { }
#endif
  }

}
