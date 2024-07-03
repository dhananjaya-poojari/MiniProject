using MongoDB.Driver;
using TinyUrl.Data;
using TinyUrl.Models;
using TinyUrl.Models.DTO;
using TinyUrl.Service.Interface;

namespace TinyUrl.Service
{
    public class TinyService : ITinyService
    {
        private readonly IMongoCollection<URL> URLs;
        private readonly IMongoCollection<User> Users;
        public TinyService(IMongoClient mongoClient, MongoDBSettings mongoDBSettings)
        {
            var database = mongoClient.GetDatabase(mongoDBSettings.DatabaseName);
            URLs = database.GetCollection<URL>("URL");
            Users = database.GetCollection<User>("User");
        }

        public string CreateNewApiKey(string email)
        {
            var user = Users.Find(item => item.EmailId == email).FirstOrDefault();
            string apiKey = string.Empty;
            if (user == null)
            {
                Guid myGuid = Guid.NewGuid();
                user = new Models.User()
                {
                    EmailId = email,
                    APIKey = myGuid.ToString()
                };
                Users.InsertOne(user);
                apiKey = myGuid.ToString();
            }
            else apiKey = user.APIKey;
            return apiKey;
        }

        public string CreateShortUrl(HttpContext context, string apiKey, RequestDTO requestDTO)
        {
            var user = Users.Find(item => item.APIKey == apiKey).FirstOrDefault() ?? throw new Exception("User is not registered");
            Guid myGuid = Guid.NewGuid();
            string encoded = myGuid.ToString().Split("-")[0];
            var url = new URL
            {
                Original = requestDTO.URL,
                EncodedHash = encoded
            };

            URLs.InsertOne(url);

            return new Uri($"{context.Request.Scheme}://{context.Request.Host}/{encoded}").ToString();
        }

        public IEnumerable<ResponseDTO> GetAll(HttpContext context, string apiKey)
        {
            var urls = URLs.Find(item => true).ToList().Select(x => new ResponseDTO
            {
                RedirectedUrl = new Uri($"{context.Request.Scheme}://{context.Request.Host}/{x.EncodedHash}").ToString(),
                OriginalUrl = x.Original,
            }).ToList();

            return urls;

        }

        public string GetShortUrl(HttpContext context, string hash)
        {
            var url = URLs.Find(x => x.EncodedHash == hash).FirstOrDefault();
            if (url == null)
            {
                throw new Exception("Invalid Url");
            }
            else
            {
                return url.Original;
            }

        }
    }
}
