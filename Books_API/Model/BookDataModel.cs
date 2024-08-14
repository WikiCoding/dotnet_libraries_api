using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Books_API.Model
{
    public class BookDataModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("reserved")]
        public bool Reserved { get; set; } = false;

        [BsonElement("library_id")]
        public string LibraryId { get; set; }
    }
}
