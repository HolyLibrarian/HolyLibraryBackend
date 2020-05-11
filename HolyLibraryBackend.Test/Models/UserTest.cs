using NUnit.Framework;
using HolyLibraryBackend.Models;

namespace HolyLibraryBackend.Test
{
    public class UserTest
    {
        private User manager;
        private User reader;

        [SetUp]
        public void Setup()
        {
            manager = new Manager();
            reader = new Reader();
        }

        [Test]
        public void TestBorrowCollection()
        {
            Collection collection = new Collection();
            Assert.IsFalse(manager.BorrowCollection(collection));
            Assert.IsTrue(reader.BorrowCollection(collection));
            Assert.IsFalse(reader.BorrowCollection(collection));
        }
    }
}
