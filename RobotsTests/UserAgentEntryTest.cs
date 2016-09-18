using System.Linq;
using Robots.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobotsTests
{
    
    
    /// <summary>
    ///This is a test class for UserAgentEntryTest and is intended
    ///to contain all UserAgentEntryTest Unit Tests
    ///</summary>
    [TestClass]
    public class UserAgentEntryTest
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
            var target = new UserAgentEntry();
            Assert.AreEqual(EntryType.UserAgent, target.Type);
        }

        [TestMethod]
        public void Create_UserAgent_Test()
        {
            var target = Entry.CreateEntry(EntryType.UserAgent);
            Assert.AreEqual(EntryType.UserAgent, target.Type);
        }

        /// <summary>
        ///A test for Entries
        ///</summary>
        [TestMethod]
        public void EntriesTest()
        {
            var target = new UserAgentEntry();
            Assert.AreEqual(0, target.Entries.Count());
        }

        /// <summary>
        ///A test for DisallowEntries
        ///</summary>
        [TestMethod]
        public void DisallowEntriesTest()
        {
            var target = new UserAgentEntry();
            Assert.AreEqual(0, target.DisallowEntries.Count());
        }

        /// <summary>
        ///A test for AllowEntries
        ///</summary>
        [TestMethod]
        public void AllowEntriesTest()
        {
            var target = new UserAgentEntry();
            Assert.AreEqual(0, target.AllowEntries.Count());
        }

        [TestMethod]
        public void Add_comment_Test()
        {
            var target = new UserAgentEntry();
            Entry entry = new CommentEntry();
            target.AddEntry(entry);
            Assert.AreNotEqual(0, target.Entries.Count());
            Assert.AreEqual(0, target.AllowEntries.Count());
            Assert.AreEqual(0, target.DisallowEntries.Count());
        }

        [TestMethod]
        public void Add_allow_entry_Test()
        {
            var target = new UserAgentEntry();
            Entry entry = new AllowEntry();
            target.AddEntry(entry);
            Assert.AreNotEqual(0, target.Entries.Count());
            Assert.AreNotEqual(0, target.AllowEntries.Count());
            Assert.AreEqual(0, target.DisallowEntries.Count());
        }

        [TestMethod]
        public void Add_disallow_entry_Test()
        {
            var target = new UserAgentEntry();
            Entry entry = new CommentEntry();
            target.AddEntry(entry);
            Assert.AreNotEqual(0, target.Entries.Count());
            Assert.AreEqual(0, target.AllowEntries.Count());
            Assert.AreEqual(0, target.DisallowEntries.Count());
        }

    }
}
