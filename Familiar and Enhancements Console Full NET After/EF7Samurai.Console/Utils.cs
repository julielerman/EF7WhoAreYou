using System;
using System.Linq;
using EF7Samurai.Context;
using EF7Samurai.Domain;

namespace EF7Samurai.ConsoleApp
{
  public class Utils
  {
    public static Samurai CreateNewSamuraiNewQuoteGraph() {
      var newSamurai = new Samurai { Name = "Gisaku" };
      newSamurai.Quotes.Add(new Quote
      { Text = @"What's the use of worrying about your beard
                                           when your head's about to be taken?" });
      return newSamurai;
    }

    public static void DisplayState(SamuraiContext context, string message) {
      Console.WriteLine();
      Console.WriteLine(message);
      context.ChangeTracker.Entries().ToList().ForEach(e =>
                      Console.WriteLine("  {0}: {1}", e.Entity.GetType(), e.State));
    }
  }
}
