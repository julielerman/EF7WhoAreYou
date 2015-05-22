using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EF7NoSQL.SamuraiDomain;
using EF7NoSQL.Data;
using System.Linq;

namespace Test
{
  [TestClass]
  public class IntegrationTests
  {
    [TestMethod, TestCategory("Integration")]
    public void CanInsertNewSamuraiIntoAzureTable() {
      var samurai = Samurai.Create();
      samurai.Name = "Julizuro";
      using (var context = new SamuraiContext()) {
        context.Samurais.Add(samurai);
        context.SaveChanges();
      }
    }

    [TestMethod, TestCategory("Integration")]
    public void CanInsertNewQuoteIntoAzureTable() {
      var quote = Quote.Create(Guid.NewGuid().ToString(), "Working towards samurai status");
      using (var context = new SamuraiContext()) {
        context.Add(quote);
        context.SaveChanges();
      }
    }

    [TestMethod, TestCategory("Integration")]
    public void CanRetrieveSamuraisFromAzureTable() {
      using (var context = new SamuraiContext()) {
        Assert.AreNotEqual(0, context.Samurais.ToList().Count);
      }
    }

    [TestMethod, TestCategory("Integration")]
    public void CanRetrieveASamuraiByIdFromAzureTable() {
      string id;
      using (var context = new SamuraiContext()) {
        id = context.Samurais.FirstOrDefault().SamuraiId;
      }
      Samurai samurai;
      using (var context = new SamuraiContext()) {
        samurai = context.Samurais.FirstOrDefault(s => s.SamuraiId == id);
      }
      Assert.AreEqual(id, samurai.SamuraiId);
    }

    [TestMethod, TestCategory("Integration")]
    public void CanUpdateSamurai() {
      string id;
      string name;
      using (var context = new SamuraiContext()) {
        var storedSamurai = context.Samurais
            .FirstOrDefault(s => s.PartitionKey == "AllSamurais");
        id = storedSamurai.SamuraiId;
        name = storedSamurai.Name;
        storedSamurai.Name += "z";
        context.SaveChanges();
      }
      using (var context = new SamuraiContext()) {
        Assert.AreEqual(name += "z",
            context.Samurais.FirstOrDefault(s => s.SamuraiId == id).Name);
      }
    }

    [TestMethod, TestCategory("Integration")]
    public void CanInsertSamuraiQuoteGraph() {
      var samurai = Samurai.Create();
      samurai.Name = "Yeti";
      samurai.AddQuote("Graph Samurai FTW!");

      using (var context = new SamuraiContext()) {
        //this version of the API doesn't have AddGraph or 
        //the AddRange method overload taking multiple entities/types
        context.Add(samurai);
        foreach (var quote in samurai.Quotes) {
          context.Add(quote);

        }
        var result = context.SaveChanges();
        Assert.AreEqual(2, result);
      }
    }
    [TestMethod, TestCategory("Integration")]
    public void CanRetrieveSamuraiQuoteGraph() {
      using (var context = new SamuraiContext()) {
        //Include not supported ...non-relational db!
        var samurai = context.Samurais
            .FirstOrDefault(s => s.Name == "Julizuro");
        var quotes = context.Quotes
            .Where(q => q.SamuraiId == samurai.SamuraiId).ToList();
        samurai.Quotes.AddRange(quotes);
        Assert.AreNotEqual(0, samurai.Quotes.Count);
      }
    }
  }
}
