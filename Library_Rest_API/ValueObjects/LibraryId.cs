using Library_Rest_API.Domain.DDD;

namespace Library_Rest_API.ValueObjects
{
    public class LibraryId : IEntityId
    {
        public Guid Id { get; }
        public LibraryId(Guid id)
        {
            Id = id;
        }
    }
}
