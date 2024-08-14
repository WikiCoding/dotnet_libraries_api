using Library_Rest_API.Domain.DDD;
using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Domain.Repo
{
    public interface ILibraryRepository : IRepository<LibraryId, Library>
    {
        Task<int> UpdateLibrary(Library library);
        Task<int> DeleteLibrary(LibraryId libraryId);
    }
}
