using Library_Rest_API.Dtos;
using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Domain.UseCases
{
    public interface IGetLibraryById
    {
        Task<LibraryDto> GetLibraryById(LibraryId id);
    }
}
