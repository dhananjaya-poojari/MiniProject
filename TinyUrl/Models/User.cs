using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TinyUrl.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("emailId")]
        public string EmailId { get; set; }

        [BsonElement("apiKey")]
        public string APIKey { get; set; }
    }
}
