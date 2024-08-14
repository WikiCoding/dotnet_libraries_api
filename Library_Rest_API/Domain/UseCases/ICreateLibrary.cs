using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Domain.UseCases
{
    public interface ICreateLibrary
    {
        Task<int> CreateLibrary(LibraryId id, LibraryName name);
    }
}
