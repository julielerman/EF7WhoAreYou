using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EF7NoSQL.SamuraiDomain;

namespace Test
{
    [TestClass]
    public class IdentityTests
    {
        [TestMethod, TestCategory("Unit")]
        public void CanCreateNewSamurai()
        {
            Assert.IsNotNull(Samurai.Create());
        }

    [TestMethod, TestCategory("Unit")]
    public void NewSamuraiHasId()
        {
            Assert.IsNotNull(Samurai.Create().SamuraiId);
        }

    [TestMethod, TestCategory("Unit")]
    public void NewSamuraiPartitionKeyReturnsAllSamurais()
        {
            var samurai = Samurai.Create();
            Assert.AreEqual("AllSamurais", samurai.PartitionKey);
        }

    [TestMethod, TestCategory("Unit")]
    public void NewQuotePartitionKeyReturnsSamuraiId()
        {
            var samuraiId = Guid.NewGuid().ToString();
            var quote = Quote.Create(samuraiId, "abc");
            Assert.AreEqual(samuraiId, quote.SamuraiId);
        }
    }
}

