using Library_Rest_API.ValueObjects;

namespace LibraryTest.ValueObjects
{
    [TestClass]
    public class LibraryNameTest
    {
        [TestMethod]
        public void LibraryNameArg_Valid()
        {
            // Arrange
            string name = "lib1";
            string expected = name;

            // Act
            LibraryName libraryName = new LibraryName(name);
            string result = libraryName.Name;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LibraryNameArg_Blank()
        {
            var obj = new LibraryName(" ");
        }

        // since string is not nullable in the VO class, then if I pass Name as null I get NullReferenceException automatically
        [TestMethod]
        public void LibraryNameArg_Null()
        {
            try
            {
                var obj = new LibraryName(null);
                Assert.Fail();
            }
            catch (NullReferenceException ex)
            {
                string exceptionExpected = "Object reference not set to an instance of an object.";
                Assert.AreEqual(exceptionExpected, ex.Message);
            }
        }

        [TestMethod]
        public void LibraryNameArg_Empty()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { new LibraryName(""); });
        }
    }
}