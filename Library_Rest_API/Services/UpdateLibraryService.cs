using Library_Rest_API.Domain;
using Library_Rest_API.Domain.Repo;
using Library_Rest_API.Domain.UseCases;
using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Services
{
    public class UpdateLibraryService : IUpdateLibrary
    {
        private readonly ILibraryFactory _libraryFactory;
        private readonly ILibraryRepository _libraryRepository;

        public UpdateLibraryService(ILibraryFactory libraryFactory, ILibraryRepository libraryRepository)
        {
            _libraryFactory = libraryFactory;
            _libraryRepository = libraryRepository;
        }

        public async Task<int> UpdateLibrary(LibraryId libraryId, LibraryName libraryName)
        {
            Library library = _libraryFactory.CreateLibrary(libraryId, libraryName);

            return await _libraryRepository.UpdateLibrary(library);
        }
    }
}
