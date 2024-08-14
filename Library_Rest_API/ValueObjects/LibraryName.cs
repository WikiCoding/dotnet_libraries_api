namespace Library_Rest_API.ValueObjects
{
    public class LibraryName
    {
        public string Name {  get; }

        public LibraryName(string name)
        {
            if (name.Trim() is null || name.Trim().Length == 0) throw new ArgumentNullException("Library name cannot be null or Empty");
            Name = name;
        }
    }
}
