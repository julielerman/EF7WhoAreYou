using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EF7Samurai.Domain;
using EF7Samurai.Context;
using System.Linq;
using Microsoft.Data.Entity;

namespace TestProject
{

  [TestClass]

  public class FunctionalTests
  {

    [TestMethod]
    public void BackwardsCompatible_DbSetAddAndAddRangeOnSingleObjects() {
      InstantiateSamurais();
      using (var context = new SamuraiContext(true)) {
        ResetContext(context);
        context.Samurais.Add(Samurai_KK);
        context.Samurais.AddRange(Samurai_KS, Samurai_S, Samurai_HH);
        context.SaveChanges();
        Assert.AreEqual(4, context.Samurais.Count());

      }
    }

    [TestMethod]
    public void New_DbContextAddAndAddRange_DetectsType() {
      InstantiateSamurais();
      using (var context = new SamuraiContext(true)) {
        ResetContext(context);
        context.Add(Samurai_KO);
        var quote = new Quote { Text = "oh my!" };
        context.AddRange(Samurai_GK, quote);
        context.SaveChanges();
        Assert.AreEqual(2, context.Samurais.Count());
        Assert.AreEqual(1, context.Quotes.Count());

      }
    }
    [TestMethod]
    public void ShowBatchCrud() {
      CreateAndSeedDbForEagerLoad();
    }


    [TestMethod]
    public void BackwardsCompatible_BasicLinqQueryingStillWorks() {
      InstantiateSamurais();
      using (var context = new SamuraiContext(true)) {
        //Arrange
        ResetContext(context);
        context.Samurais.AddRange(Samurai_KS, Samurai_S, Samurai_HH, Samurai_GK);
        context.SaveChanges();

        //Act
        var samurais_UpperCaseShi = context.Samurais.Where(s => s.Name.Contains("Shi")).ToList();
        //Assert
        Console.WriteLine("Samurais with Upper Case 'Shi' in name");
        samurais_UpperCaseShi.ForEach(s => Console.WriteLine(s.Name));
        Assert.AreEqual(2, samurais_UpperCaseShi.Count);
        Console.WriteLine("____________________");
        //Assert
        var samurai = context.Samurais.FirstOrDefault(s => s.Name.Contains("shi"));

        Assert.AreEqual(samurai.Name, "Heihachi Hayashida");

      }
    }

    private void CreateAndSeedDbForEagerLoad() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      var aMaker = new Maker { Name = "A Maker" };
      Samurai_GK.Swords.Add(new Sword { Maker = aMaker, WeightGrams = 100 });
      using (var context = new SamuraiContext()) {
        ResetContext(context);
        context.Makers.Add(aMaker);
        context.Samurais.Add(Samurai_GK);
        context.SaveChanges();

      }
    }

    [TestMethod]
    public void BackwardsCompatible_EagerLoadingWithImprovements() {
      //Note: Explicit Loading is backlog for RTM
      //using database for these tests so inmemory doesn't populate context
      //Arrange
      CreateAndSeedDbForEagerLoad();

      //Act
      using (var context = new SamuraiContext()) {
        var samurai = context.Samurais.Include(s => s.Quotes)
                                      .FirstOrDefault();
        Assert.AreEqual(1, samurai.Quotes.Count);
      }
      using (var context = new SamuraiContext()) {
        var samurai = context.Samurais.Include(s => s.Swords)
                                  .Include(s => s.Quotes)
                                  .FirstOrDefault();
        Assert.AreEqual(1, samurai.Quotes.Count);
        Assert.AreEqual(1, samurai.Swords.Count);
      }


      using (var context = new SamuraiContext()) {
        var samurai = context.Samurais
          .Include(s => s.Swords).ThenInclude(s => s.Maker)
          .FirstOrDefault();

        Assert.AreEqual(1, samurai.Swords.Count);
        Assert.IsNotNull(samurai.Swords[0].Maker);

      }
    }
    [TestMethod, TestCategory("DisconnectedGraphs")]
    public void BackwardsCompatible_DbSetAddAndAddRangeOnGraphs() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      using (var context = new SamuraiContext(true)) {
        ResetContext(context);
        context.Samurais.Add(Samurai_GK);
        context.SaveChanges();
        Assert.AreEqual(1, context.Samurais.Count());
        Assert.AreEqual(1, context.Quotes.Count());
      }
    }
    [TestMethod, TestCategory("DisconnectedGraphs")]
    public void New_DisconnectedPatterns_DbSetAdd_EnumToSpecifyRootOnly() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      using (var context = new SamuraiContext(true)) {
        ResetContext(context);
        context.Samurais.Add(Samurai_GK, GraphBehavior.SingleObject); //<--this should NOT make Quote added
        context.SaveChanges();
        Assert.AreEqual(1, context.Samurais.Count());
        Assert.AreEqual(0, context.Quotes.Count());
      }
    }

    [TestMethod, TestCategory("DisconnectedGraphs")]
    public void New_DisconnectedPatterns_DbSetAddRange_EnumToSpecifyRootOnly() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      using (var context = new SamuraiContext(true)) {
        ResetContext(context);
        context.Samurais.AddRange(new[] { Samurai_KK, Samurai_GK }, behavior: GraphBehavior.SingleObject);
        context.SaveChanges();
        Assert.AreEqual(2, context.Samurais.Count());
        Assert.AreEqual(0, context.Quotes.Count());
      }
    }

    [TestMethod, TestCategory("DisconnectedGraphs")]
    public void New_DisconnectedPatterns_EntryAdd_AddsRootOnly() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      using (var context = new SamuraiContext(true)) {
        ResetContext(context);
        context.Entry(Samurai_GK).State = EntityState.Added;
        context.SaveChanges();
        Assert.AreEqual(1, context.Samurais.Count());
        Assert.AreEqual(0, context.Quotes.Count());

      }
    }
    [TestMethod]
    public void New_DisconnectedPatterns_AttachNewGraphViaChangeTracker() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      using (var context = new SamuraiContext()) {
        ResetContext(context);
        context.ChangeTracker.TrackGraph(Samurai_GK,
          e => e.Entry.State = EntityState.Added);
        context.SaveChanges();
        Assert.AreEqual(1, context.Samurais.Count());
        Assert.AreEqual(1, context.Quotes.Count());
      }
    }








    private void ResetContext(SamuraiContext context) {
      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();
    }

    private void InstantiateSamurais() {
      Samurai_KK = new Samurai { Name = "Kikuchiyo" };
      Samurai_KS = new Samurai { Name = "Kambei Shimada" };
      Samurai_S = new Samurai { Name = "Shichirōji" };
      Samurai_KO = new Samurai { Name = "Katsushirō Okamoto" };
      Samurai_HH = new Samurai { Name = "Heihachi Hayashida" };
      Samurai_KZ = new Samurai { Name = "Kyūzō" };
      Samurai_GK = new Samurai { Name = "Gorōbei Katayama" };
    }

    Samurai Samurai_KK;
    Samurai Samurai_KS;
    Samurai Samurai_S;
    Samurai Samurai_KO;
    Samurai Samurai_HH;
    Samurai Samurai_KZ;
    Samurai Samurai_GK;

    private enum ProviderType
    {
      InMemory,
      SqlServer

    }

    private DbContextOptionsBuilder InMemoryOptions(ProviderType type) {
      var ob = new DbContextOptionsBuilder();

      switch (type) {
        case ProviderType.InMemory:
          ob.UseInMemoryDatabase();

          break;
        case ProviderType.SqlServer:
          ob.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database=EF7SamuraiConsole; Trusted_Connection=True; MultipleActiveResultSets = True;")
           .MaxBatchSize(40);
          break;
        default:
          break;
      }

      return ob;

    }
  }
}




