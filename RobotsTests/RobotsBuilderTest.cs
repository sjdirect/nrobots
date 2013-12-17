using Robots.Fluent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Assert = Xunit.Assert;

namespace RobotsTests
{
    
    
    /// <summary>
    ///This is a test class for RobotsBuilderTest and is intended
    ///to contain all RobotsBuilderTest Unit Tests
    ///</summary>
    [TestClass]
    public class RobotsBuilderTest
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


        /// <summary>
        ///A test for Allow
        ///</summary>
        [TestMethod]
        public void Building_robots_Test()
        {
            var baseUri = new Uri("http://www.microsoft.com");
            var robots = RobotsBuilder.Create(baseUri)
                .ForUserAgent("bot1")
                .Disallow("/web")
                .Disallow("/cloud")
                .Allow("/web/page1.aspx")

                .ForUserAgent("bot2")
                .Disallow("/")

                .ForUserAgent("*")
                .DisallowWithComment("/blocked", "with comment")

                .Robots;

            Assert.NotNull(robots);
            Assert.Equal(false, robots.Allowed("/web", "bot1"));
            Assert.Equal(false, robots.Allowed("/web/allowed", "bot1"));
            Assert.Equal(true, robots.Allowed("/web/page1.aspx", "bot1"));
            
            Assert.Equal(false, robots.Allowed("/", "bot2"));
            Assert.Equal(false, robots.Allowed("/page.aspx", "bot2"));
            
            Assert.Equal(false, robots.Allowed("/blocked"));
            Assert.Equal(false, robots.Allowed("/blocked", "*"));
            Assert.Equal(true, robots.Allowed("/notblocked"));
        }
    }
}
