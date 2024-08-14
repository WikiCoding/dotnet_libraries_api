using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Domain.UseCases
{
    public interface IDeleteLibrary
    {
        Task<int> DeleteLibrary(LibraryId id);
    }
}
