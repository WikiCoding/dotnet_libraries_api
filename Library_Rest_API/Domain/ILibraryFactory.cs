using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Domain
{
    public interface ILibraryFactory
    {
        Library CreateLibrary(LibraryId libraryId, LibraryName libraryName);
    }
}
