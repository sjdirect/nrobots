using Robots.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Assert = Xunit.Assert;

namespace RobotsTests
{
    
    
    /// <summary>
    ///This is a test class for UrlEntryTest and is intended
    ///to contain all UrlEntryTest Unit Tests
    ///</summary>
    [TestClass]
    public class UrlEntryTest
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
        ///A test for Url
        ///</summary>
        [TestMethod]
        public void UrlTest()
        {
            UrlEntry target = CreateUrlEntry(); // TODO: Initialize to an appropriate value
            var expected = new Uri("http://www.microsoft.com");
            target.Url = expected;
            Uri actual = target.Url;
            Assert.Equal(expected, actual);
       }

        internal virtual UrlEntry CreateUrlEntry()
        {
            UrlEntry target = new AllowEntry();
            return target;
        }

        /// <summary>
        ///A test for Inverted
        ///</summary>
        [TestMethod]
        public void InvertedTest()
        {
            UrlEntry target = CreateUrlEntry(); // TODO: Initialize to an appropriate value
            const bool expected = true;
            target.Inverted = expected;
            bool actual = target.Inverted;
            Assert.Equal(expected, actual);
        }
    }
}
