using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TinyUrl.Models
{
    public class URL
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("original")]
        public string Original { get; set; }

        [BsonElement("encodedHash")]
        public string EncodedHash { get; set; }
    }
}
