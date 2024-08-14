using Library_Rest_API.Domain;
using Library_Rest_API.Domain.Repo;
using Library_Rest_API.Domain.UseCases;
using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Services
{
    public class CreateLibraryService(ILibraryFactory libraryFactory, ILibraryRepository libraryRepository) : ICreateLibrary
    {
        private readonly ILibraryFactory _libraryFactory = libraryFactory;
        private readonly ILibraryRepository _libraryRepository = libraryRepository;

        public async Task<int> CreateLibrary(LibraryId id, LibraryName name)
        {
            Library library = _libraryFactory.CreateLibrary(id, name);

            int rowsAffected = await _libraryRepository.SaveAsync(library);

            return rowsAffected;
        }
    }
}
