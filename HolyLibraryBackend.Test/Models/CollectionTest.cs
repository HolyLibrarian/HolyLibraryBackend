using NUnit.Framework;
using HolyLibraryBackend.Models;

namespace HolyLibraryBackend.Test
{
    public class CollectionTest
    {
        private Collection collection;

        [SetUp]
        public void Setup()
        {
            collection = new Collection();
        }

        [Test]
        public void TestIsBorrowed()
        {
            Assert.IsFalse(collection.IsBorrowed());
            var user = new Reader();
            collection.Borrower = user;
            Assert.IsTrue(collection.IsBorrowed());
        }
    }
}
