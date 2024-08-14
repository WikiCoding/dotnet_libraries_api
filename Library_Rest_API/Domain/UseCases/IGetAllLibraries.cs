using Library_Rest_API.Dtos;

namespace Library_Rest_API.Domain.UseCases
{
    public interface IGetAllLibraries
    {
        Task<IEnumerable<LibraryDto>> GetAllLibraries();
    }
}
