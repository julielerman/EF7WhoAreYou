using Microsoft.Data.Entity;
using System;
using System.IO;
using Windows.Storage;

namespace CookieCounterApp
{
    public class BingeContext : DbContext
    {
        public DbSet<CookieBinge> Binges { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
      string databaseFilePath = "CookieBinge.db";
      try {
        databaseFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseFilePath);
      }
      catch (InvalidOperationException) { }


      options.UseSqlite($"Data source={databaseFilePath}");
        }
    }
}


