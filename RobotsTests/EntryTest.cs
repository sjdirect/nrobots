using Robots.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasCommentTest()
        {
            Entry target = new UserAgentEntry(); // TODO: Initialize to an appropriate value
            bool actual = target.HasComment;
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void HasCommentTest2()
        {
            Entry target = new UserAgentEntry {Comment = "comment"};
            bool actual = target.HasComment;
            Assert.AreEqual(true, actual);
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
            Assert.IsNotNull(entry);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(EntryType.UserAgent, entry.Type);
            Assert.AreEqual("*", ((UserAgentEntry)entry).UserAgent);
        }

        [TestMethod]
        public void TryParse_empty_UserAgent_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "User-Agent:";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.IsNull(entry);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void TryParse_good_disallow_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "disallow: /web  #comment";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.IsNotNull(entry);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(EntryType.Disallow, entry.Type);
            Assert.IsNotNull(((DisallowEntry)entry).Url);
            Assert.AreEqual("/web", ((DisallowEntry)entry).Url.LocalPath);
            Assert.AreEqual("comment", entry.Comment);
        }

        [TestMethod]
        public void TryParse_good_disallow_empty_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "disallow:";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.AreEqual(null, entry);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void TryParse_good_disallow_whitespace_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "disallow:  ";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.AreEqual(null, entry);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void TryParse_good_allow_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "allow: /web";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.IsNotNull(entry);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(EntryType.Allow, entry.Type);
            Assert.IsNotNull(((AllowEntry)entry).Url);
            Assert.AreEqual("/web", ((AllowEntry)entry).Url.LocalPath);
        }

        [TestMethod]
        public void TryParse_comment_allow_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            const string entryText = "#comment";
            Entry entry;
            bool actual = Entry.TryParse(baseUri, entryText, out entry);
            Assert.IsNotNull(entry);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(EntryType.Comment, entry.Type);
            Assert.AreEqual("comment", entry.Comment);
        }

        [TestMethod]
        public void TryParse_null_base_uri_Test()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                Entry entry;
                Entry.TryParse(null, "", out entry);
            });
        }

        [TestMethod]
        public void TryParse_empty_string_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            Entry entry;
            bool actual = Entry.TryParse(baseUri, "", out entry);
            Assert.AreEqual(false, actual);
            Assert.IsNull(entry);
        }
    }
}
