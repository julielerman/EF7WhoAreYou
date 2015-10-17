using System;
using System.Linq;
using EF7Samurai.Domain;
using EF7Samurai.Context;
using Microsoft.Data.Entity;

namespace EF7Samurai.ConsoleApp
{
  class Program
  {
    static void Main(string[] args) {
     


      PlayWithContext();
      Batch_CUD();
      AttachNewGraphUsingEntry();
      AttachNewGraphViaChangeTracker();
    
    }

    static Samurai Samurai_KK = new Samurai { Name = "Kikuchiyo" };
    static Samurai Samurai_KS = new Samurai { Name = "Kambei Shimada" };
    static Samurai Samurai_S = new Samurai { Name = "Shichirōji" };
    static Samurai Samurai_KO = new Samurai { Name = "Katsushirō Okamoto" };
    static Samurai Samurai_HH = new Samurai { Name = "Heihachi Hayashida" };
    static Samurai Samurai_KZ = new Samurai { Name = "Kyūzō" };
    static Samurai Samurai_GK = new Samurai { Name = "Gorōbei Katayama" };




    private static void PlayWithContext() {
      using (var context = new SamuraiContext()) {
        context.Samurais.Add(Samurai_KK);
        context.Add(Samurai_KO); 

        context.Samurais.AddRange(Samurai_KS, Samurai_S, Samurai_HH);
                
        var quote = new Quote { Text = "oh my!" };
        context.AddRange(Samurai_GK, quote);
        context.SaveChanges();
        Console.WriteLine("Total Samurais: {0}", context.Samurais.Count());
        Console.WriteLine("Total Quotes: {0}", context.Quotes.Count());

        var samurais = context.Samurais.Where(s => s.Name.Contains("Shi")).ToList();
        samurais.ForEach(s => Console.WriteLine(s.Name));
        var samurai = context.Samurais.FirstOrDefault(s => s.Name.Contains("Shi"));
        Console.WriteLine(samurai.Name);
        Console.WriteLine("Play with Context method complete. Press ENTER to continue...");
        Console.ReadLine();
      }
    }


    private static void Batch_CUD() {
      //Initializers are gone. Use Migraitons!
      //for testing, these Explicit methods are great.
      //in presentation, I discussed that magic db init is gone from EF7
      //dropping & recreating the db takes a little extra time
      //note that I created a custom overload to ensure SQLServer is used


      using (var context = new SamuraiContext(true)) {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
      }
      Console.WriteLine("If you want to see the effect of this method, switch the context builder options to the UseSqlServer code and also start your db profiler. Press any key to continue...");

      using (var context = new SamuraiContext(true)) {
        context.Samurais.AddRange(Samurai_KK, Samurai_KS, Samurai_GK);
        context.SaveChanges();
      }

      using (var context = new SamuraiContext(true)) {
        var samurais = context.Samurais.ToList();
        samurais[0].Name += "The Glorious";
        var ks = samurais.FirstOrDefault(n => n.Name.Contains("Shimada"));
        ks.Quotes.Add(
            new Quote
            {
              Text =
                        @"This is the nature of war. By protecting others,
                          you save yourself. If you only think of yourself, 
                          you’ll only destroy yourself."
            });
        context.Remove(samurais.Where(n => n.Name.Contains("Gor")).FirstOrDefault());
        var newSamurai = new Samurai { Name = "Julizuro" };
        //Note that the way Add etc works wrt keys behind the scenes
        //is evolving still but will be quite different then previous
        //versions of EF.
        //This works with the beta4
        //may change again though. Details here: https://github.com/aspnet/EntityFramework/issues/2210

        context.Entry(newSamurai).State=EntityState.Added;
        context.SaveChanges();

     }
      Console.WriteLine("Check profiler to see the batched up SQL sent to db.");
      Console.WriteLine("Batch method complete. Press ENTER to continue to next demo");
      Console.ReadLine();
    }

    private static void AttachNewGraphUsingEntry() {
      Samurai newSamurai = Utils.CreateNewSamuraiNewQuoteGraph();
      using (var context = new SamuraiContext()) {
         context.Entry(newSamurai).State=EntityState.Added;
        Utils.DisplayState(context, "Entry/State Attach New Graph");
        Console.WriteLine("Attach new Graph with Entry method complete. ");
        Console.ReadKey();

      }

    }
    private static void AttachNewGraphViaChangeTracker() {
      Samurai newSamurai = Utils.CreateNewSamuraiNewQuoteGraph();
      using (var context = new SamuraiContext()) {
        context.ChangeTracker.TrackGraph(newSamurai,
          e => e.NodeState=EntityState.Added); 
        Utils.DisplayState(context, "AttachGraph (no callback) New Graph");
        Console.WriteLine("AttachGraph demo complete. Press ENTER to continue to next demo");
        Console.ReadKey();
      }
    }








  }
}
//       context.ChangeTracker.Entries().ToList().ForEach(e=>System.Console.WriteLine(e.State));
