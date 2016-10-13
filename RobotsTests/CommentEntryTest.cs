using Robots.Model;
using Xunit;
namespace RobotsTests
{
    
    
    /// <summary>
    ///This is a test class for CommentEntryTest and is intended
    ///to contain all CommentEntryTest Unit Tests
    ///</summary>
    public class CommentEntryTest
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
            var target = new CommentEntry();
            Assert.Equal(EntryType.Comment, target.Type);
        }

        [Fact]
        public void Create_CommentEntry_Test()
        {
            var target = Entry.CreateEntry(EntryType.Comment);
            Assert.Equal(EntryType.Comment, target.Type);
        }
    }
}
