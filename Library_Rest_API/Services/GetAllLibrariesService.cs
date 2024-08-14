using Library_Rest_API.Domain;
using Library_Rest_API.Domain.Repo;
using Library_Rest_API.Domain.UseCases;
using Library_Rest_API.Dtos;

namespace Library_Rest_API.Services
{
    public class GetAllLibrariesService : IGetAllLibraries
    {
        private readonly ILibraryRepository _libraryRepository;

        public GetAllLibrariesService(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        public async Task<IEnumerable<LibraryDto>> GetAllLibraries()
        {
            IEnumerable<Library> libraries = await _libraryRepository.GetAllAsync();

            return new LibraryMapper().ListDomainToDataModel(libraries);
        }
    }
}
