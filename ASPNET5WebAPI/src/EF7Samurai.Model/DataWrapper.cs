using EF7Samurai.Domain;
using Microsoft.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace EF7Samurai.Model
{
  public class NonTrackingDataWrapper
    {
    private SamuraiContext _context;
    public NonTrackingDataWrapper(SamuraiContext context) {
      _context = context;
    }

    public List<Samurai> GetSamuraisWithQuotes() {
      return _context.Samurais.Include(s => s.Quotes).AsNoTracking().ToList();
    }
    public Samurai GetSamurai(int id) {
      return _context.Samurais.AsNoTracking().FirstOrDefault(s => s.Id == id);
    }

    }
}
