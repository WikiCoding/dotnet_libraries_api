using Books_API.Dto;
using Books_API.Exceptions;
using Books_API.Infra;
using Books_API.Model;
using Books_API.Repository;
using MongoDB.Bson;

namespace Books_API.Services
{
    public class BookService
    {
        private const string LIBRARY_SERVICE_BASE_URL = "http://localhost:5199/api/v1/libraries";
        private readonly IBookRepository _bookRepository;
        private readonly ILogger _logger;
        private readonly Client _client;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger, Client client)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _client = client;
        }

        public async Task<BookDataModel> CreateBookService(string title, string libraryId)
        {
            await ValidateLibraryIdExists(libraryId);

            BookDataModel bookDataModel = new() { Title = title, LibraryId = libraryId };

            var response = await _bookRepository.SaveBookAsync(bookDataModel);

            return response;
        }

        public async Task<IEnumerable<BookDataModel>> GetAllBooksService()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<BookDataModel> GetBookByIdService(string _id)
        {
            return await _bookRepository.GetBookByIdAsync(_id);
        }

        public async Task<BookDataModel> UpdateBookService(string id, string title, string libraryId)
        {
            await ValidateLibraryIdExists(libraryId);

            BookDataModel bookDataModel = new() { _id = ObjectId.Parse(id), Title = title, LibraryId = libraryId };

            var response = await _bookRepository.UpdateBookAsync(bookDataModel);

            return response;
        }

        public async Task<BookDataModel> DeleteBookById(string _id)
        {
            return await _bookRepository.DeleteBookAsync(_id);
        }

        private async Task ValidateLibraryIdExists(string libraryId)
        {
            LibrariesResponse? library = await _client.GetAsync(libraryId);

            if (library == null)
            {
                _logger.LogError("Library not found");
                throw new EntityNotFoundException("Library not found");
            }

            _logger.LogInformation(message: "Library data was found an is avaliable: " + "id:" + library.Id + " name:" + library.Name);
        }

    }
}
