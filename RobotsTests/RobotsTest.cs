using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace RobotsTests
{
    /// <summary>
    ///This is a test class for RobotsTest and is intended
    ///to contain all RobotsTest Unit Tests
    ///</summary>
    [TestClass]
    public class RobotsTest
    {
        private const string BASE_URL = "http://www.test.com";
        private static Robots.Robots _robotsContent;
        private static Robots.Robots _robotsUrl;
        private static Robots.Robots _robotsEmpty;
        private static Robots.Robots _robotsWildcards;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        
        private static Robots.Robots RobotsContent
        {
            get
            {
                if (_robotsContent == null)
                {
                    _robotsContent = new Robots.Robots();
                    _robotsContent.LoadContent(
        @"User-Agent: *
Disallow: /disallowedfile.txt
Disallow: /disallowedfolder
Disallow: /disallowedfolder/subfolder
Allow: /allowedfile2.txt
Allow: /allowedfolder",
                        BASE_URL
                        );
                }
                return _robotsContent;
            }
        }

        private static Robots.Robots RobotsUrl
        {
            get
            {
                if (_robotsUrl == null)
                {
                    _robotsUrl = new Robots.Robots();
                    _robotsUrl.Load(new Uri(@"http://www.bing.com/robots.txt"));
                }
                return _robotsUrl;
            }
        }

        private static Robots.Robots RobotsEmpty
        {
            get
            {
                if (_robotsEmpty == null)
                {
                    _robotsEmpty = new Robots.Robots();
                    _robotsEmpty.LoadContent("User-Agent: *\r\nDisallow: ", BASE_URL);
                }
                return _robotsEmpty;
            }
        }

        private static Robots.Robots RobotsWildcards
        {
            get
            {
                if (_robotsWildcards == null)
                {
                    _robotsWildcards = new Robots.Robots();
                    _robotsWildcards.LoadContent(@"User-Agent: *
Disallow: */private/
Disallow: /a/*/b/
Disallow: /a/*$"
                                                 , BASE_URL);
                }
                return _robotsWildcards;
            }
        }


        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
        }
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
        public void Create_robots_with_content_Test()
        {
            var robots = RobotsContent;
        }

        [TestMethod]
        public void Create_robots_with_all_robots_out_content_Test()
        {
            var robots = RobotsEmpty;
        }

        [TestMethod]
        public void Create_robots_with_uri_Test()
        {
            var robots = RobotsUrl;
        }

        [TestMethod]
        public void Disallow_root_all_robots_out_content_Test()
        {
            bool actual = RobotsEmpty.Allowed(BASE_URL + "/");
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Disallow_folder_all_robots_out_content_Test()
        {
            bool actual = RobotsEmpty.Allowed(BASE_URL + "/folder");
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Disallow_file_all_robots_out_content_Test()
        {
            bool actual = RobotsEmpty.Allowed(BASE_URL + "/folder/file.txt");
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Disallow_private_folder_wildcard_content_Test()
        {
            bool actual = RobotsWildcards.Allowed("/folder/private");
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Disallow_subfolder_wildcard_content_Test()
        {
            bool actual = RobotsWildcards.Allowed("/a/private/b/");
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Allow_subfolder_wildcard_content_Test()
        {
            bool actual = RobotsWildcards.Allowed("/a/public/c/");
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Explicit_disallowed_file_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/disallowedfile.txt");
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Implicit_allowed_file_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/allowedfile.xml");
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Explicit_disallowed_folder_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/disallowedfolder");
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Implicit_allowed_folder_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/allowedfolder");
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Explicit_allowed_file_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/allowedfile2.txt");
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Explicit_allowed_folder_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/allowedfolder2");
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Explicit_relative_disallowed_file_Test()
        {
            bool actual = RobotsContent.Allowed("/disallowedfile.txt");
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Uri_loaded_disallow_subfolder_Test()
        {
            bool actual = RobotsUrl.Allowed("/recipe/pizza");
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Uri_loaded_allow_folder_Test()
        {
            bool actual = RobotsUrl.Allowed("/entertainment");
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Uri_loaded_allow_subfolder_Test()
        {
            bool actual = RobotsUrl.Allowed("/entertainment/music");
            Assert.AreEqual(true, actual);
        }
    }
}
