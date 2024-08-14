using Library_Rest_API.Domain;
using Library_Rest_API.ValueObjects;

namespace LibraryTest.Domain
{
    [TestClass]
    public class LibraryTest
    {
        private readonly ILibraryFactory _libraryFactory;
        public LibraryTest()
        {
            _libraryFactory = new LibraryFactoryImpl();     
        }
        
        [TestMethod]
        public void CreateLibrary_Valid()
        {
            // Arrange
            Guid guid = Guid.NewGuid();
            string name = "lib1";
            LibraryId libraryId = new LibraryId(guid);
            LibraryName libraryName = new LibraryName(name);

            // Act
            Library library = _libraryFactory.CreateLibrary(libraryId, libraryName);

            // Assert
            Assert.AreEqual(guid, library.LibraryId.Id);
            Assert.AreEqual(name, library.LibraryName.Name);
        }

        [TestMethod]
        public void CreateLibrary_NullLibraryId()
        {
            // Arrange
            string name = "lib1";
            LibraryName libraryName = new LibraryName(name);

            Library library = _libraryFactory.CreateLibrary(null, libraryName);

            // Act & Assert
            Assert.IsNull(library.LibraryId);
        }

        [TestMethod]
        public void CreateLibrary_NullLibraryName()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            LibraryId libraryId = new LibraryId(id);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => { _libraryFactory.CreateLibrary(libraryId, null); });
        }
    }
}
