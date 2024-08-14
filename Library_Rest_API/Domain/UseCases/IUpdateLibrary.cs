using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Domain.UseCases
{
    public interface IUpdateLibrary
    {
        Task<int> UpdateLibrary(LibraryId libraryId, LibraryName libraryName);
    }
}
