using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookieCounterApp
{
    public class BingeService
    {
        public static void RecordBinge(int count, bool worthIt)
        {
            var binge = new CookieBinge
            {
                HowMany=count,
                WorthIt=worthIt,
                TimeOccurred = DateTime.Now
            };

            using (var db = new BingeContext())
            {
                db.Binges.Add(binge);
                db.SaveChanges();
            }
        }

        public static IEnumerable<CookieBinge> GetLast5Binges()
        {
            using (var db = new BingeContext())
            {
                var latestBinges = db.Binges
                    .OrderByDescending(b=>b.TimeOccurred)
                    .Take(5);

                return latestBinges;
            }
        }
    }
}
