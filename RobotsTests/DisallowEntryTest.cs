using Robots.Model;
using Xunit;
namespace RobotsTests
{
    
    
    /// <summary>
    ///This is a test class for DisallowEntryTest and is intended
    ///to contain all DisallowEntryTest Unit Tests
    ///</summary>
    public class DisallowEntryTest
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
            var target = new DisallowEntry();
            Assert.Equal(EntryType.Disallow, target.Type);
        }

        [Fact]
        public void Create_DisallowEntry_Test()
        {
            var target = Entry.CreateEntry(EntryType.Disallow);
            Assert.Equal(EntryType.Disallow, target.Type);
        }

    }
}
