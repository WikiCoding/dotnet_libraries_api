using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Domain
{
    public class LibraryFactoryImpl : ILibraryFactory
    {
        public Library CreateLibrary(LibraryId libraryId, LibraryName libraryName)
        {
            return new Library(libraryId, libraryName);
        }
    }
}
