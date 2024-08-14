using Library_Rest_API.Domain;
using Library_Rest_API.Domain.Repo;
using Library_Rest_API.Dtos;
using Library_Rest_API.Persistence.Datamodel;
using Library_Rest_API.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Library_Rest_API.Persistence.Repository
{
    public class LibraryRepositoryImpl : ILibraryRepository
    {
        private readonly ILogger _logger;
        private readonly LibraryDbContext _libraryDbContext;

        public LibraryRepositoryImpl(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            IEnumerable<LibraryDataModel> libraryDataModels = await _libraryDbContext.Libraries.ToListAsync();

            return new LibraryMapper().ListDatamodelToDomain(libraryDataModels);
        }

        public async Task<Library?> GetByIdAsync(LibraryId id)
        {
            LibraryDataModel? model = await _libraryDbContext.Libraries.Where(lib => lib.Id == id.Id).FirstOrDefaultAsync();

            if (model == null) return null;

            return new LibraryMapper().DatamodelToDomain(model);
        }

        public async Task<int> SaveAsync(Library obj)
        {
            LibraryDataModel model = new(obj)
            {
                Id = Guid.Empty
            };

            var tx = _libraryDbContext.Database.BeginTransaction();

            var saved = _libraryDbContext.Add(model);

            int rowsAffected = await _libraryDbContext.SaveChangesAsync();

            if (rowsAffected == 0)
            {
                tx.Rollback();
                throw new Exception($"There was a problem adding");
            }

            tx.Commit();

            return rowsAffected;
        }

        public async Task<int> UpdateLibrary(Library library)
        {
            var tx = _libraryDbContext.Database.BeginTransaction();

            try
            {
                int rowsAffected = await _libraryDbContext.Libraries.Where(lib => lib.Id == library.LibraryId.Id)
                    .ExecuteUpdateAsync(updates => updates.SetProperty(lib => lib.Name, library.LibraryName.Name)
                                                          .SetProperty(lib => lib.Version, lib => lib.Version + 1));

                if (rowsAffected == 0)
                {
                    tx.Rollback();
                    throw new Exception("Item not Found or Concurrency error occurred while updating the library.");
                }

                tx.Commit();

                return rowsAffected;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                tx.Rollback();
                throw new Exception("Concurrency error occurred while updating the library.", ex);
            }
        }

        public async Task<int> DeleteLibrary(LibraryId libraryId)
        {
            var tx = _libraryDbContext.Database.BeginTransaction();
            try
            {
                int rowsAffected = await _libraryDbContext.Libraries.Where(lib => lib.Id == libraryId.Id)
                    .ExecuteDeleteAsync();

                if (rowsAffected == 0) throw new Exception("Item not found.");

                tx.Commit();

                return rowsAffected;
            } catch (DbUpdateConcurrencyException ex)
            {
                tx.Rollback();
                _logger?.LogError("An error occurred while deleting library: {Message}", ex.Message);
                throw;
            } catch (Exception ex) 
            {
                tx.Rollback();
                _logger?.LogError("An error occurred while deleting library: {Message}", ex.Message);
                throw;
            }
        }
    }
}
