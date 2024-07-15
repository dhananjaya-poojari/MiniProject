using mongodb;
using MongoDB.Driver;
using Server.Models;
using System.Text;

namespace Server.Service
{
    public class PasteService : IPasteService
    {
        private readonly IMongoCollection<Paste> Pastes;
        public PasteService(IMongoClient mongoClient, MongoDBSettings mongoDBSettings)
        {
            var database = mongoClient.GetDatabase(mongoDBSettings.DatabaseName);
            Pastes = database.GetCollection<Paste>("Paste");
        }

        public string Get(string id)
        {
            string contents = File.ReadAllText(GetFilePath(id + ".txt"));
            return contents;
        }

        public string Upload(HttpContext context, string content)
        {
            Guid myGuid = Guid.NewGuid();
            string filename = myGuid.ToString().Split("-")[0] + ".txt";

            File.WriteAllText(GetFilePath(filename), content);

            //var paste = new Paste
            //{
            //    Filename = filename.Replace(".txt", ""),
            //};

            //Pastes.InsertOne(paste);

            return new Uri($"{context.Request.Scheme}://{context.Request.Host}/{filename.Replace(".txt", "")}").ToString();
        }

        private string GetFilePath(string fileName)
        {
            var filePath = @"wwwroot\Paste\" + fileName;
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\wwwroot\Paste\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\wwwroot\Paste\");
            }
            var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            return fileDirectory;
        }
    }
}
