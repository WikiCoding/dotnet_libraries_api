using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Domain
{
    public class Library
    {
        public LibraryId LibraryId { get; }
        public LibraryName LibraryName { get; }

        internal Library(LibraryId libraryId, LibraryName libraryName)
        {
            if (libraryName is null) throw new ArgumentNullException("Null Library Name");
            LibraryId = libraryId;
            LibraryName = libraryName;
        }
    }
}
