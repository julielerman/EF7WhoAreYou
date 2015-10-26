using System;
using System.Collections.Generic;
using System.Text;

namespace CookieCounterApp
{
    public class CookieBinge
    {
        public Guid Id { get; set; }
        public DateTime TimeOccurred { get; set; }
        public int HowMany { get; set; }
        public bool WorthIt { get; set; }

    }
}
