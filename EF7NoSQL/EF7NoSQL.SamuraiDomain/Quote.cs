using System;

namespace EF7NoSQL.SamuraiDomain
{
    public class Quote 
    {
        public static Quote Create(string samuraiKey,string text)
        { 
            return new Quote( samuraiKey, text);
        }

        private Quote( string samuraiKey, string text)
        {
            QuoteId = Guid.NewGuid().ToString();
            Text = text;
            SamuraiId = samuraiKey;
        }

        public Quote() { }
        public string QuoteId { get; private set; }
        public string Text { get; set; }
        public string SamuraiId { get; private set; }
    }
}