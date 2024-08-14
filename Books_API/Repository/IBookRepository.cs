using Books_API.Dto;
using Books_API.Model;

namespace Books_API.Repository
{
    public interface IBookRepository
    {
        Task<BookDataModel> SaveBookAsync(BookDataModel book);

        Task<IEnumerable<BookDataModel>> GetAllBooksAsync();

        Task<BookDataModel> UpdateBookAsync(BookDataModel book);

        Task<BookDataModel> DeleteBookAsync(string _id);

        Task<BookDataModel> GetBookByIdAsync(string _id);
    }
}
