using Library_Rest_API.Domain.Repo;
using Library_Rest_API.Domain.UseCases;
using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Services
{
    public class DeleteLibraryService : IDeleteLibrary
    {
        private readonly ILibraryRepository _libraryRepository;

        public DeleteLibraryService(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public async Task<int> DeleteLibrary(LibraryId id)
        {
            return await _libraryRepository.DeleteLibrary(id);
        }
    }
}
