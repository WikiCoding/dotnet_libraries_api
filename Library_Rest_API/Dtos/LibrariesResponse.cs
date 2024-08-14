namespace Library_Rest_API.Dtos
{
    public class LibrariesResponse
    {
        public LibrariesResponse(Guid? id, string? name)
        {
            Id = id;
            Name = name;
        }
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
