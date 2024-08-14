using Library_Rest_API.Domain;
using Library_Rest_API.Persistence.Datamodel;
using Library_Rest_API.ValueObjects;

namespace Library_Rest_API.Dtos
{
    public class LibraryMapper
    {
        public Library DatamodelToDomain(LibraryDataModel libraryDataModel) { 
            ILibraryFactory libraryFactory = new LibraryFactoryImpl();

            LibraryId libraryId = new(libraryDataModel.Id);
            LibraryName libraryName = new(libraryDataModel.Name);

            return libraryFactory.CreateLibrary(libraryId, libraryName);
        }

        public IEnumerable<Library> ListDatamodelToDomain(IEnumerable<LibraryDataModel> libraryDataModels)
        {
            return libraryDataModels.ToList()
                .ConvertAll(libDm => DatamodelToDomain(libDm));
        }

        public LibraryDto DomainToDataModel(Library library) 
        {
            return new LibraryDto
            {
                Id = library.LibraryId.Id,
                Name = library.LibraryName.Name
            };
        }

        public IEnumerable<LibraryDto> ListDomainToDataModel(IEnumerable<Library> libraries)
        {
            return libraries.ToList()
                .ConvertAll<LibraryDto>(lib => DomainToDataModel(lib));
        }

    }
}
