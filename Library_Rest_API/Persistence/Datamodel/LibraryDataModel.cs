using Library_Rest_API.Domain;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Library_Rest_API.Persistence.Datamodel
{
    public class LibraryDataModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [NotNull]
        [Required]
        public string Name { get; set; }

        [ConcurrencyCheck]
        public int Version { get; set; }

        public LibraryDataModel()
        {
            
        }

        public LibraryDataModel(Library library)
        {
            Id = library.LibraryId.Id;
            Name = library.LibraryName.Name;
        }
    }
}
