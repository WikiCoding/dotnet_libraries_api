namespace Books_API.Dto
{
    public class BookRes
    {
        public string _id { get; set; }
        public string Title { get; set; }
        public bool IsReserved { get; set; }
        public string LibraryId { get; set; }
    }
}
