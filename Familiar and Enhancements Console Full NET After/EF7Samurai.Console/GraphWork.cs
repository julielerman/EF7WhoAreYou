using System;
using System.Linq;
using EF7Samurai.Domain;
using EF7Samurai.Context;
using System.Collections.Generic;
using Microsoft.Data.Entity;

namespace EF7Samurai.ConsoleApp
{

   public class GraphWork
    {
        static Samurai Samurai_KK = new Samurai { Name = "Kikuchiyo" };
        static Samurai Samurai_KS = new Samurai { Name = "Kambei Shimada" };
        static Samurai Samurai_S = new Samurai { Name = "Shichirōji" };
        static Samurai Samurai_KO = new Samurai { Name = "Katsushirō Okamoto" };
        static Samurai Samurai_HH = new Samurai { Name = "Heihachi Hayashida" };
        static Samurai Samurai_KZ = new Samurai { Name = "Kyūzō" };
        static Samurai Samurai_GK = new Samurai { Name = "Gorōbei Katayama" };

        private static void TrackingChangesWhileConnected()
        {
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(Samurai_KK);
                System.Console.WriteLine("1)" + context.Entry(Samurai_KK).State);
                context.SaveChanges();
                System.Console.WriteLine("2)" + context.Entry(Samurai_KK).State);
                Samurai_KK.Name += "No.7! ";
                System.Console.WriteLine("3)" + context.Entry(Samurai_KK).State);
                context.Samurais.Remove(Samurai_KK);
                System.Console.WriteLine("4)" + context.Entry(Samurai_KK).State);
                System.Console.ReadLine();
            }
        }
        private static void DisconnectedGraphMethods()
        {
            AttachNewGraphUsingDbSetAdd();
            AttachNewGraphUsingDbContextAdd();
            AttachNewGraphUsingEntry();
            AttachNewGraphViaChangeTracker();
            AttachGraphWithExistingParentNewChild();
            Console.ForegroundColor = ConsoleColor.Cyan;
            AttachExistingGraphWithModifiedParent();
            AttachGraphWithModifiedParentAndCallBack();
            AddGraphWithModifiedChild();
            Console.Write("Press any key to continue...");
            Console.Read();
        }

      


        private static void AttachNewGraphUsingDbContextAdd()
        {
            Samurai newSamurai = Utils.CreateNewSamuraiNewQuoteGraph();
            using (var context = new SamuraiContext())
            {
                context.Add(newSamurai);
                Utils.DisplayState(context, "DbContext.Add New Graph");
            }
        }

        private static void AttachNewGraphUsingDbSetAdd()
        {
            Samurai newSamurai = Utils.CreateNewSamuraiNewQuoteGraph();
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(newSamurai);
                Utils.DisplayState(context, "DbSet.Add New Graph");
            }
        }

        private static void AttachNewGraphUsingEntry()
        {
            Samurai newSamurai = Utils.CreateNewSamuraiNewQuoteGraph();
            using (var context = new SamuraiContext())
            {
                //EF4.3 -> EF6: context.Entry(newSamurai).State == EntityState.Added;
                context.Entry(newSamurai).SetState(EntityState.Added);
                Utils.DisplayState(context, "Entry/State Attach New Graph");
            }
        }

        private static void AttachNewGraphViaChangeTracker()
        {
            Samurai newSamurai = Utils.CreateNewSamuraiNewQuoteGraph();
            using (var context = new SamuraiContext())
            {
                context.ChangeTracker.AttachGraph(newSamurai);
                Utils.DisplayState(context, "AttachGraph (no callback) New Graph");
            }
        }

        private static Samurai GetExistingParentChildGraph()
        {
            Samurai samurai;
            using (var context = new SamuraiContext())
            {
                samurai = context.Samurais
                            .Include(s => s.Quotes)
                            .FirstOrDefault(s => s.Quotes.Any());
            }
            return samurai;
        }

        private static void AttachExistingGraphWithModifiedParent()
        {
            Samurai samurai = GetExistingParentChildGraph();
            samurai.Name += " Modified";
            using (var context = new SamuraiContext())
            {
                context.ChangeTracker.AttachGraph(samurai);
                Utils.DisplayState(context, "Attach Graph (no callback) with modified parent");
            }
        }

        private static void AttachGraphWithExistingParentNewChild()
        {
            Samurai samurai;
            using (var context = new SamuraiContext())
            {
                samurai = context.Samurais.FirstOrDefault(s => s.Name.Contains("Shimada"));
            }
            samurai.Quotes.Add(new Quote { Text = "Danger always strikes when everything seems fine." });
            using (var context = new SamuraiContext())
            {
                context.ChangeTracker.AttachGraph(samurai);
                Utils.DisplayState(context, "Attach Graph (no callback) existing unchanged parent, new child");
            }
        }

        private static void AttachGraphWithModifiedParentAndCallBack()
        {
            Samurai samurai = GetExistingParentChildGraph();
            samurai.Name += " Modified";
            using (var context = new SamuraiContext())
            {
                context.ChangeTracker.AttachGraph(samurai,
                    e => e.SetState(EntityState.Modified));
               Utils.DisplayState(context, "Attach Graph using Callback with modified parent, unchanged child");
            }
        }
        private static void AddGraphWithModifiedChild()
        {
            Samurai samurai = GetExistingParentChildGraph();
            samurai.Quotes[0].Text += "Modified";
            using (var context = new SamuraiContext())
            {
                context.ChangeTracker.AttachGraph(samurai);
                Utils.DisplayState(context, "Attach Graph (no callback) unchanged parent with modified child");
            }
        }



        private static void DisconnectedEntities()
        {
            //Setup
            List<Samurai> disconnectedSamurais;
            using (var context = new SamuraiContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Samurais.Add(Samurai_KK, Samurai_KS, Samurai_GK, Samurai_KK, Samurai_KZ);
                Samurai_KK.Quotes.Add(new Quote { Text = "I'm the funny one" });
                context.SaveChanges();
            }
            using (var context = new SamuraiContext())
            {
                //Include requires MARS support in connection string
                disconnectedSamurais = context.Samurais.Include(s => s.Quotes).ToList();

            }
            //work with disconnected samurais

            var modifiedSamurai = disconnectedSamurais[0];
            modifiedSamurai.Name += "The Glorious";

            var modifiedQuote = disconnectedSamurais.FirstOrDefault(s => s.Quotes.Any()).Quotes.FirstOrDefault();
            modifiedQuote.Text += "Julizuro was here!";

            var ksId = disconnectedSamurais.FirstOrDefault(n => n.Name.Contains("Shimada")).Id;
            var newQuote = new Quote
            {
                Text =
                             @"This is the nature of war. By protecting others,
                                  you save yourself. If you only think of yourself, 
                                  you’ll only destroy yourself.",
                SamuraiId = ksId

            };
            var newSamurai = new Samurai { Name = "Julizuro" };
            using (var context = new SamuraiContext())
            {
                context.Add(newQuote, newSamurai);
                context.Update(modifiedQuote, modifiedSamurai);

            }

        }
       

   




        private static void TrackingGraphChangesWhileConnected()
        {
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(Samurai_KK);
                Samurai_KK.Quotes.Add(new Quote { Text = "I can't kill a lot with 1 sword!" });
                System.Console.Write("1)");
                DisplayState(context);

                context.SaveChanges();
                Samurai_KK.Quotes.Add(new Quote { Text = "You call yourself a horse!" });
                System.Console.Write("2)");
                DisplayState(context);

                Samurai_KK.Quotes[0].Text = "I can't kill a lot with one sword!!";
                System.Console.Write("3)");
                DisplayState(context);

                //Note: With the alpha version I am using of the SQL SErver Provider,
                //there seems to be a bug that is preventing this from working. 
                //It works correctly with the in memory
                //provider. Follow the issue on github here: https://github.com/aspnet/EntityFramework/issues/1449
                //context.Remove(Samurai_KK.Quotes[1]);
                //System.Console.Write("4)");
                //DisplayState(context);
                System.Console.ReadLine();
            }
        }

        private static void DisplayState(SamuraiContext context)
        {
            context.ChangeTracker.Entries().ToList().ForEach(e =>
                            System.Console.WriteLine("  {0}: {1}", e.Entity.GetType(), e.State));
        }

    }
}
