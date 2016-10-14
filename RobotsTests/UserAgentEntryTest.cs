using Robots.Model;
using  Xunit;

namespace RobotsTests
{ 
    /// <summary>
    ///This is a test class for UserAgentEntryTest and is intended
    ///to contain all UserAgentEntryTest Unit Tests
    ///</summary>
    public class UserAgentEntryTest
    {
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


        [Fact]
        public void EntryType_Test()
        {
            var target = new UserAgentEntry();
            Assert.Equal(EntryType.UserAgent, target.Type);
        }

        [Fact]
        public void Create_UserAgent_Test()
        {
            var target = Entry.CreateEntry(EntryType.UserAgent);
            Assert.Equal(EntryType.UserAgent, target.Type);
        }

        /// <summary>
        ///A test for Entries
        ///</summary>
        [Fact]
        public void EntriesTest()
        {
            var target = new UserAgentEntry();
            Assert.Empty(target.Entries);
        }

        /// <summary>
        ///A test for DisallowEntries
        ///</summary>
        [Fact]
        public void DisallowEntriesTest()
        {
            var target = new UserAgentEntry();
            Assert.Empty(target.DisallowEntries);
        }

        /// <summary>
        ///A test for AllowEntries
        ///</summary>
        [Fact]
        public void AllowEntriesTest()
        {
            var target = new UserAgentEntry();
            Assert.Empty(target.AllowEntries);
        }

        [Fact]
        public void Add_comment_Test()
        {
            var target = new UserAgentEntry();
            Entry entry = new CommentEntry();
            target.AddEntry(entry);
            Assert.NotEmpty(target.Entries);
            Assert.Empty(target.AllowEntries);
            Assert.Empty(target.DisallowEntries);
        }

        [Fact]
        public void Add_allow_entry_Test()
        {
            var target = new UserAgentEntry();
            Entry entry = new AllowEntry();
            target.AddEntry(entry);
            Assert.NotEmpty(target.Entries);
            Assert.NotEmpty(target.AllowEntries);
            Assert.Empty(target.DisallowEntries);
        }

        [Fact]
        public void Add_disallow_entry_Test()
        {
            var target = new UserAgentEntry();
            Entry entry = new CommentEntry();
            target.AddEntry(entry);
            Assert.NotEmpty(target.Entries);
            Assert.Empty(target.AllowEntries);
            Assert.Empty(target.DisallowEntries);
        }

    }
}
