using EF7NoSQL.SamuraiDomain;
using Microsoft.Data.Entity;
using System.Runtime.CompilerServices;
using System.Configuration;
using Microsoft.Data.Entity.Metadata;

[assembly: InternalsVisibleTo("Test")]
namespace EF7NoSQL.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
 
        protected override void OnConfiguring(DbContextOptions options)
        {
            options.UseAzureTableStorage
                (ConfigurationManager
                   .ConnectionStrings["samuraiConnection"].ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
              .ForAzureTableStorage()
              .PartitionAndRowKey(s => s.PartitionKey, s => s.SamuraiId);

         modelBuilder.Entity<Quote>()
           .ForAzureTableStorage()
           .PartitionAndRowKey(q => q.SamuraiId, q => q.QuoteId).Table("Samurai");
        }
    }
}



