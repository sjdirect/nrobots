using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


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
        private static Robots.Robots _robotsCrawlDelay;
        private static Robots.Robots _robotsSitemap;
        private static Robots.Robots _robotsSitemapMultiple;
        private static Robots.Robots _robotsGroupedUserAgents;
        private static Robots.Robots _robotsContentWithQuerystringOnRoot;

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

        private static Robots.Robots RobotsCrawlDelay
        {
            get
            {
                if (_robotsCrawlDelay == null)
                {
                    _robotsCrawlDelay = new Robots.Robots();
                    _robotsCrawlDelay.LoadContent(
        @"User-Agent: *
Crawl-Delay: 20

User-Agent: userAgentCrawlDelayIs1
Crawl-Delay: 1

User-Agent: userAgentCrawlDelayNotSpecified
Allow: /

User-Agent: userAgentCrawlDelayEmpty
Crawl-Delay: ",
                        BASE_URL
                        );
                }
                return _robotsCrawlDelay;
            }
        }

        private static Robots.Robots RobotsSitemap
        {
            get
            {
                if (_robotsSitemap == null)
                {
                    _robotsSitemap = new Robots.Robots();
                    _robotsSitemap.LoadContent(
@"Sitemap: http://a.com/sitemap.xml
",
                        BASE_URL
                        );
                }
                return _robotsSitemap;
            }
        }

        private static Robots.Robots RobotsSitemapMultiple
        {
            get
            {
                if (_robotsSitemapMultiple == null)
                {
                    _robotsSitemapMultiple = new Robots.Robots();
                    _robotsSitemapMultiple.LoadContent(
@"Sitemap: http://a.com/sitemap.xml
Sitemap: http://b.com/sitemap.xml
Sitemap: http://c.com/sitemap.xml
",
                        BASE_URL
                        );
                }
                return _robotsSitemapMultiple;
            }
        }

        private static Robots.Robots RobotsGroupedUserAgents
        {
            get
            {
                if (_robotsGroupedUserAgents == null)
                {
                    _robotsGroupedUserAgents = new Robots.Robots();
                    _robotsGroupedUserAgents.LoadContent(
        @"User-Agent: msnbot
User-Agent: googlebot
Disallow: /aaa

User-Agent: slurp
User-Agent: blahblah
Disallow: /bbb
",
                        BASE_URL
                        );
                }
                return _robotsGroupedUserAgents;
            }
        }

        private static Robots.Robots RobotsContentWithQuerystringOnRoot
        {
            get
            {
                if (_robotsContentWithQuerystringOnRoot == null)
                {
                    _robotsContentWithQuerystringOnRoot = new Robots.Robots();
                    _robotsContentWithQuerystringOnRoot.LoadContent(
        @"User-Agent: *
Disallow: /?category=whatever
Disallow: /?category=another&color=red
",
                        BASE_URL
                        );
                }
                return _robotsContentWithQuerystringOnRoot;
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
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Disallow_folder_all_robots_out_content_Test()
        {
            bool actual = RobotsEmpty.Allowed(BASE_URL + "/folder");
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Disallow_file_all_robots_out_content_Test()
        {
            bool actual = RobotsEmpty.Allowed(BASE_URL + "/folder/file.txt");
            Assert.AreEqual(true, actual);
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
        public void Disallow_root_grouped_agents_robots_content_Test()
        {
            Assert.IsFalse(RobotsGroupedUserAgents.Allowed(BASE_URL + "/aaa", "googlebot"));
            Assert.IsFalse(RobotsGroupedUserAgents.Allowed(BASE_URL + "/aaa", "msnbot"));
            Assert.IsTrue(RobotsGroupedUserAgents.Allowed(BASE_URL + "/bbb", "googlebot"));
            Assert.IsTrue(RobotsGroupedUserAgents.Allowed(BASE_URL + "/bbb", "msnbot"));

            Assert.IsFalse(RobotsGroupedUserAgents.Allowed(BASE_URL + "/bbb", "slurp"));
            Assert.IsFalse(RobotsGroupedUserAgents.Allowed(BASE_URL + "/bbb", "blahblah"));
            Assert.IsTrue(RobotsGroupedUserAgents.Allowed(BASE_URL + "/aaa", "slurp"));
            Assert.IsTrue(RobotsGroupedUserAgents.Allowed(BASE_URL + "/aaa", "blahblah")); 
        }

        [TestMethod]
        public void Disallow_querystring_on_root_allows_root_Test()
        {
            Assert.IsTrue(RobotsContentWithQuerystringOnRoot.Allowed(BASE_URL, "googlebot"));
        }

        [TestMethod]
        public void Disallow_querystring_exact_match_not_supported_Test()
        {
            Assert.IsTrue(RobotsContentWithQuerystringOnRoot.Allowed(BASE_URL + "?category=whatever", "googlebot"));
        }

        [TestMethod]
        public void Disallow_querystring_nonexact_match_not_supported_Test()
        {
            Assert.IsTrue(RobotsContentWithQuerystringOnRoot.Allowed(BASE_URL + "?category=another&blah=blah", "googlebot"));
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

        [TestMethod, Ignore]//No longer valid, bing doesn't have these entries
        public void Uri_loaded_disallow_subfolder_Test()
        {
            Assert.AreEqual(false, RobotsUrl.Allowed("/cashback/admin"));
            Assert.AreEqual(false, RobotsUrl.Allowed("/cashback/admin/aaa/tool.html"));
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

        [TestMethod]
        public void CrawlDelay_NoUserAgentParam_UsesWildcardUserAgentCrawlDelayValue()
        {
            Assert.AreEqual(20, RobotsCrawlDelay.GetCrawlDelay());
        }

        [TestMethod]
        public void CrawlDelay_NoCrawlDelayValueInRobotsDotText_ReturnZero()
        {
            Assert.AreEqual(0, RobotsEmpty.GetCrawlDelay());
        }

        [TestMethod]
        public void CrawlDelay_NonExistentUserAgent_UsesWildcardCrawlDelayValue()
        {
            Assert.AreEqual(20, RobotsCrawlDelay.GetCrawlDelay("nonExistentUserAgent"));
        }

        [TestMethod]
        public void CrawlDelay_UserAgentCrawlDelayIs1_Returns1()
        {
            Assert.AreEqual(1, RobotsCrawlDelay.GetCrawlDelay("userAgentCrawlDelayIs1"));
        }

        [TestMethod]
        public void CrawlDelay_UserAgentCrawlDelayNotSpecified_ReturnsZero()
        {
            Assert.AreEqual(0, RobotsContent.GetCrawlDelay("userAgentCrawlDelayNotSpecified"));
        }

        [TestMethod]
        public void CrawlDelay_UserAgentCrawlDelayEmpty_ReturnsZero()
        {
            Assert.AreEqual(0, RobotsContent.GetCrawlDelay("userAgentCrawlDelayEmpty"));
        }


        [TestMethod]
        public void SitemapUrl_SingleSitemapPresent_ReturnsUrl()
        {
            Assert.AreEqual("http://a.com/sitemap.xml", RobotsSitemap.GetSitemapUrls()[0]);
        }

        [TestMethod]
        public void SitemapUrl_NoSpaceBetween_ReturnsUrl()
        {
            Robots.Robots robots = new Robots.Robots();
            robots.LoadContent(@"Sitemap:http://a.com/sitemap.xml", BASE_URL);

            Assert.AreEqual("http://a.com/sitemap.xml", robots.GetSitemapUrls()[0]);
        }

        [TestMethod]
        public void SitemapUrl_MultipleSpaceBetween_ReturnsUrl()
        {
            Robots.Robots robots = new Robots.Robots();
            robots.LoadContent(@"Sitemap:   http://a.com/sitemap.xml", BASE_URL);

            Assert.AreEqual("http://a.com/sitemap.xml", robots.GetSitemapUrls()[0]);
        }

        [TestMethod]
        public void SitemapUrl_NoUrl_Returns1EntryThatIsEmtpty()
        {
            Robots.Robots robots = new Robots.Robots();
            robots.LoadContent(@"Sitemap:  ", BASE_URL);

            Assert.AreEqual("", robots.GetSitemapUrls()[0]);
        }

        [TestMethod]
        public void SitemapUrl_KeywordCase_ReturnsUrl()
        {
            Robots.Robots robots = new Robots.Robots();
            robots.LoadContent(@"siteMAp: http://a.com/sitemap.xml", BASE_URL);

            Assert.AreEqual("http://a.com/sitemap.xml", robots.GetSitemapUrls()[0]);
        }

        [TestMethod]
        public void SitemapUrl_SitemapPresent_ReturnsUrls()
        {
            IList<string> result = RobotsSitemapMultiple.GetSitemapUrls();

            Assert.AreEqual("http://a.com/sitemap.xml", result[0]);
            Assert.AreEqual("http://b.com/sitemap.xml", result[1]);
            Assert.AreEqual("http://c.com/sitemap.xml", result[2]);
        }

        [TestMethod]
        public void Sitemap_NoSitemapValueInRobotsDotText_ReturnEmptyCollection()
        {

            Assert.AreEqual(0, RobotsEmpty.GetSitemapUrls().Count);
        }
    }
}
