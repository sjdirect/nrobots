using Robots.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Assert = Xunit.Assert;

namespace RobotsTests
{
    
    
    /// <summary>
    ///This is a test class for EntryTest and is intended
    ///to contain all EntryTest Unit Tests
    ///</summary>
    [TestClass]
    public class EntryTest
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
        public void CommentTest()
        {
            Entry target = new UserAgentEntry(); // TODO: Initialize to an appropriate value
            const string expected = "comment";
            target.Comment = expected;
            string actual = target.Comment;
            Assert.Equal(expected, actual);
        }

        [TestMethod]
        public void HasCommentTest()
        {
            Entry target = new UserAgentEntry(); // TODO: Initialize to an appropriate value
            bool actual = target.HasComment;
            Assert.Equal(false, actual);
        }

        [TestMethod]
        public void HasCommentTest2()
        {
            Entry target = new UserAgentEntry {Comment = "comment"};
            bool actual = target.HasComment;
            Assert.Equal(true, actual);
        }

        internal virtual Entry CreateEntry()
        {
            // TODO: Instantiate an appropriate concrete class.
            Entry target = null;
            return target;
        }


        [TestMethod]
        public void TryParse_good_UserAgent_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "User-Agent: *";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.NotNull(entry);
            Assert.Equal(true, actual);
            Assert.Equal(EntryType.UserAgent, entry.Type);
            Assert.Equal("*", ((UserAgentEntry)entry).UserAgent);
        }

        [TestMethod]
        public void TryParse_empty_UserAgent_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "User-Agent:";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.Null(entry);
            Assert.Equal(false, actual);
        }

        [TestMethod]
        public void TryParse_good_disallow_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "disallow: /web  #comment";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.NotNull(entry);
            Assert.Equal(true, actual);
            Assert.Equal(EntryType.Disallow, entry.Type);
            Assert.NotNull(((DisallowEntry)entry).Url);
            Assert.Equal("/web", ((DisallowEntry)entry).Url.LocalPath);
            Assert.Equal("comment", entry.Comment);
        }

        [TestMethod]
        public void TryParse_good_disallow_empty_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "disallow:";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.Equal(null, entry);
            Assert.Equal(false, actual);
        }

        [TestMethod]
        public void TryParse_good_disallow_whitespace_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "disallow:  ";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.Equal(null, entry);
            Assert.Equal(false, actual);
        }

        [TestMethod]
        public void TryParse_good_allow_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "allow: /web";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.NotNull(entry);
            Assert.Equal(true, actual);
            Assert.Equal(EntryType.Allow, entry.Type);
            Assert.NotNull(((AllowEntry)entry).Url);
            Assert.Equal("/web", ((AllowEntry)entry).Url.LocalPath);
        }

        [TestMethod]
        public void TryParse_comment_allow_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "#comment";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.NotNull(entry);
            Assert.Equal(true, actual);
            Assert.Equal(EntryType.Comment, entry.Type);
            Assert.Equal("comment", entry.Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryParse_null_base_uri_Test()
        {
            Entry entry;
            Entry.TryParse(null, "", out entry);
        }

        [TestMethod]
        public void TryParse_empty_string_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            Entry entry;
            bool actual = Entry.TryParse(baseUri, "", out entry);
            Assert.Equal(false, actual);
            Assert.Null(entry);
        }
    }
}
