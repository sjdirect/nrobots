using Robots.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RobotsTests
{


    /// <summary>
    ///This is a test class for AllowEntryTest and is intended
    ///to contain all AllowEntryTest Unit Tests
    ///</summary>
    [TestClass]
    public class AllowEntryTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod]
        public void EntryType_Test()
        {
            var target = new AllowEntry();
            Assert.AreEqual(EntryType.Allow, target.Type);
        }

        [TestMethod]
        public void Create_AllowEntry_Test()
        {
            var target = Entry.CreateEntry(EntryType.Allow);
            Assert.AreEqual(EntryType.Allow, target.Type);
        }

    }
}