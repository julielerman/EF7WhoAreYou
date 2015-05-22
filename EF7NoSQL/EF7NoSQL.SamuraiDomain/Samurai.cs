using System;
using System.Collections.Generic;

namespace EF7NoSQL.SamuraiDomain
{
    public class Samurai
    {
        public static Samurai Create()
        {
            return new Samurai();
        }

        public Samurai()
        {
            SamuraiId = Guid.NewGuid().ToString();
            PartitionKey = "AllSamurais";
            Quotes = new List<Quote>();
        }

        public void AddQuote(string quoteText)
        {
            Quotes.Add(Quote.Create(SamuraiId, quoteText));
        }
        public string SamuraiId { get; private set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        public string PartitionKey { get; private set; }

    }
}
