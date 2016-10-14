using System;
using System.Collections.Generic;
using Xunit;

namespace RobotsTests
{
    /// <summary>
    ///This is a test class for RobotsTest and is intended
    ///to contain all RobotsTest Unit Tests
    ///</summary>
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
Disallow: /disallowedcompletefolder/
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
        public void Create_robots_with_content_Test()
        {
            var robots = RobotsContent;
        }

        [Fact]
        public void Create_robots_with_all_robots_out_content_Test()
        {
            var robots = RobotsEmpty;
        }

        [Fact]
        public void Create_robots_with_uri_Test()
        {
            var robots = RobotsUrl;
        }

        [Fact]
        public void Disallow_root_all_robots_out_content_Test()
        {
            bool actual = RobotsEmpty.Allowed(BASE_URL + "/");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void Disallow_folder_all_robots_out_content_Test()
        {
            bool actual = RobotsEmpty.Allowed(BASE_URL + "/folder");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void Disallow_file_all_robots_out_content_Test()
        {
            bool actual = RobotsEmpty.Allowed(BASE_URL + "/folder/file.txt");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void Disallow_private_folder_wildcard_content_Test()
        {
            bool actual = RobotsWildcards.Allowed("/folder/private");
            Assert.Equal(false, actual);
        }

        [Fact]
        public void Disallow_subfolder_wildcard_content_Test()
        {
            bool actual = RobotsWildcards.Allowed("/a/private/b/");
            Assert.Equal(false, actual);
        }

        [Fact]
        public void Disallow_root_grouped_agents_robots_content_Test()
        {
            Assert.False(RobotsGroupedUserAgents.Allowed(BASE_URL + "/aaa", "googlebot"));
            Assert.False(RobotsGroupedUserAgents.Allowed(BASE_URL + "/aaa", "msnbot"));
            Assert.True(RobotsGroupedUserAgents.Allowed(BASE_URL + "/bbb", "googlebot"));
            Assert.True(RobotsGroupedUserAgents.Allowed(BASE_URL + "/bbb", "msnbot"));

            Assert.False(RobotsGroupedUserAgents.Allowed(BASE_URL + "/bbb", "slurp"));
            Assert.False(RobotsGroupedUserAgents.Allowed(BASE_URL + "/bbb", "blahblah"));
            Assert.True(RobotsGroupedUserAgents.Allowed(BASE_URL + "/aaa", "slurp"));
            Assert.True(RobotsGroupedUserAgents.Allowed(BASE_URL + "/aaa", "blahblah")); 
        }

        [Fact]
        public void Disallow_querystring_on_root_allows_root_Test()
        {
            Assert.True(RobotsContentWithQuerystringOnRoot.Allowed(BASE_URL, "googlebot"));
        }

        [Fact]
        public void Disallow_querystring_exact_match_not_supported_Test()
        {
            Assert.True(RobotsContentWithQuerystringOnRoot.Allowed(BASE_URL + "?category=whatever", "googlebot"));
        }

        [Fact]
        public void Disallow_querystring_nonexact_match_not_supported_Test()
        {
            Assert.True(RobotsContentWithQuerystringOnRoot.Allowed(BASE_URL + "?category=another&blah=blah", "googlebot"));
        }


        [Fact]
        public void Allow_subfolder_wildcard_content_Test()
        {
            bool actual = RobotsWildcards.Allowed("/a/public/c/");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void Explicit_disallowed_file_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/disallowedfile.txt");
            Assert.Equal(false, actual);
        }

        [Fact]
        public void Implicit_allowed_file_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/allowedfile.xml");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void Explicit_disallowed_folder_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/disallowedfolder");
            Assert.Equal(false, actual);
        }
        
        [Fact]
        public void Explicit_disallowed_complete_folder_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/disallowedcompletefolder");
            Assert.Equal(false, actual);
        }
        
        [Fact]
        public void Explicit_disallowed_complete_folder_Test_different_folder()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/disallowedcompletefolderwithmoretext");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void Implicit_allowed_folder_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/allowedfolder");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void Explicit_allowed_file_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/allowedfile2.txt");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void Explicit_allowed_folder_Test()
        {
            bool actual = RobotsContent.Allowed(BASE_URL + "/allowedfolder2");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void Explicit_relative_disallowed_file_Test()
        {
            bool actual = RobotsContent.Allowed("/disallowedfile.txt");
            Assert.Equal(false, actual);
        }        

        [Fact]
        public void Uri_loaded_allow_folder_Test()
        {
            bool actual = RobotsUrl.Allowed("/entertainment");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void Uri_loaded_allow_subfolder_Test()
        {
            bool actual = RobotsUrl.Allowed("/entertainment/music");
            Assert.Equal(true, actual);
        }

        [Fact]
        public void CrawlDelay_NoUserAgentParam_UsesWildcardUserAgentCrawlDelayValue()
        {
            Assert.Equal(20, RobotsCrawlDelay.GetCrawlDelay());
        }

        [Fact]
        public void CrawlDelay_NoCrawlDelayValueInRobotsDotText_ReturnZero()
        {
            Assert.Equal(0, RobotsEmpty.GetCrawlDelay());
        }

        [Fact]
        public void CrawlDelay_NonExistentUserAgent_UsesWildcardCrawlDelayValue()
        {
            Assert.Equal(20, RobotsCrawlDelay.GetCrawlDelay("nonExistentUserAgent"));
        }

        [Fact]
        public void CrawlDelay_UserAgentCrawlDelayIs1_Returns1()
        {
            Assert.Equal(1, RobotsCrawlDelay.GetCrawlDelay("userAgentCrawlDelayIs1"));
        }

        [Fact]
        public void CrawlDelay_UserAgentCrawlDelayNotSpecified_ReturnsZero()
        {
            Assert.Equal(0, RobotsContent.GetCrawlDelay("userAgentCrawlDelayNotSpecified"));
        }

        [Fact]
        public void CrawlDelay_UserAgentCrawlDelayEmpty_ReturnsZero()
        {
            Assert.Equal(0, RobotsContent.GetCrawlDelay("userAgentCrawlDelayEmpty"));
        }


        [Fact]
        public void SitemapUrl_SingleSitemapPresent_ReturnsUrl()
        {
            Assert.Equal("http://a.com/sitemap.xml", RobotsSitemap.GetSitemapUrls()[0]);
        }

        [Fact]
        public void SitemapUrl_NoSpaceBetween_ReturnsUrl()
        {
            Robots.Robots robots = new Robots.Robots();
            robots.LoadContent(@"Sitemap:http://a.com/sitemap.xml", BASE_URL);

            Assert.Equal("http://a.com/sitemap.xml", robots.GetSitemapUrls()[0]);
        }

        [Fact]
        public void SitemapUrl_MultipleSpaceBetween_ReturnsUrl()
        {
            Robots.Robots robots = new Robots.Robots();
            robots.LoadContent(@"Sitemap:   http://a.com/sitemap.xml", BASE_URL);

            Assert.Equal("http://a.com/sitemap.xml", robots.GetSitemapUrls()[0]);
        }

        [Fact]
        public void SitemapUrl_NoUrl_Returns1EntryThatIsEmtpty()
        {
            Robots.Robots robots = new Robots.Robots();
            robots.LoadContent(@"Sitemap:  ", BASE_URL);

            Assert.Equal("", robots.GetSitemapUrls()[0]);
        }

        [Fact]
        public void SitemapUrl_KeywordCase_ReturnsUrl()
        {
            Robots.Robots robots = new Robots.Robots();
            robots.LoadContent(@"siteMAp: http://a.com/sitemap.xml", BASE_URL);

            Assert.Equal("http://a.com/sitemap.xml", robots.GetSitemapUrls()[0]);
        }

        [Fact]
        public void SitemapUrl_SitemapPresent_ReturnsUrls()
        {
            IList<string> result = RobotsSitemapMultiple.GetSitemapUrls();

            Assert.Equal("http://a.com/sitemap.xml", result[0]);
            Assert.Equal("http://b.com/sitemap.xml", result[1]);
            Assert.Equal("http://c.com/sitemap.xml", result[2]);
        }

        [Fact]
        public void Sitemap_NoSitemapValueInRobotsDotText_ReturnEmptyCollection()
        {

            Assert.Equal(0, RobotsEmpty.GetSitemapUrls().Count);
        }
    }
}
