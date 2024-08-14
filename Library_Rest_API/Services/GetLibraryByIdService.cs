using Library_Rest_API.Domain;
using Library_Rest_API.Domain.Repo;
using Library_Rest_API.Domain.UseCases;
using Library_Rest_API.Dtos;
using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Services
{
    public class GetLibraryByIdService : IGetLibraryById
    {
        private readonly ILibraryRepository _libraryRepository;

        public GetLibraryByIdService(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public async Task<LibraryDto> GetLibraryById(LibraryId id)
        {
            Library? library = await _libraryRepository.GetByIdAsync(id);
            if (library == null) throw new Exception("Library not found");

            return new LibraryDto
            {
                Id = library.LibraryId.Id,
                Name = library.LibraryName.Name
            };
        }
    }
}
