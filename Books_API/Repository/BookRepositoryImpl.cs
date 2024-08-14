using Books_API.Exceptions;
using Books_API.Model;
using Books_API.Persistence;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Books_API.Repository
{
    public class BookRepositoryImpl : IBookRepository
    {
        private readonly BookDbContext _bookDbContext;

        public BookRepositoryImpl(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }

        public async Task<IEnumerable<BookDataModel>> GetAllBooksAsync()
        {
            return await _bookDbContext.Books.ToListAsync();
        }

        public async Task<BookDataModel> SaveBookAsync(BookDataModel book)
        {
            _bookDbContext.Books.Add(book);

            await SaveDbUpdates();

            return book;
        }

        public async Task<BookDataModel> UpdateBookAsync(BookDataModel book)
        {
            BookDataModel bookDataModel = await GetBookByIdAsync(book._id.ToString());

            bookDataModel.Title = book.Title;
            bookDataModel.LibraryId = book.LibraryId;

            await SaveDbUpdates();

            return bookDataModel;

        }

        public async Task<BookDataModel> DeleteBookAsync(string _id)
        {
            BookDataModel bookDataModel = await GetBookByIdAsync(_id);

            _bookDbContext.Books.Remove(bookDataModel);
            
            await SaveDbUpdates();

            return bookDataModel;
        }

        public async Task<BookDataModel> GetBookByIdAsync(string _id)
        {
            BookDataModel? bookDataModel = await _bookDbContext.Books.FindAsync(ObjectId.Parse(_id));

            if (bookDataModel == null)
            {
                throw new EntityNotFoundException("Book not found");
            }

            return bookDataModel;
        }

        private async Task SaveDbUpdates()
        {
            int rowsAffected = await _bookDbContext.SaveChangesAsync();

            if (rowsAffected == 0)
            {
                throw new DbUpdateException("Error updating.");
            }
        }
    }
}
