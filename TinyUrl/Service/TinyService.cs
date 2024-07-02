using Microsoft.EntityFrameworkCore;
using TinyUrl.Data;
using TinyUrl.Models;
using TinyUrl.Models.DTO;
using TinyUrl.Service.Interface;

namespace TinyUrl.Service
{
    public class TinyService : ITinyService
    {
        private AppDBContext _dbContext;
        public TinyService(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<string> CreateNewApiKey(string name)
        {
            Guid myGuid = Guid.NewGuid();
            var user = new Models.User()
            {
                APIKey = myGuid.ToString()
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return myGuid.ToString();
        }

        public async Task<string> CreateShortUrl(HttpContext context, string apiKey, RequestDTO requestDTO)
        {
            var user = _dbContext.Users.Where(x => x.APIKey == apiKey).FirstOrDefault() ?? throw new Exception("User is not registered");
            Guid myGuid = Guid.NewGuid();
            string encoded = myGuid.ToString().Split("-")[0];
            var url = new URL
            {
                Original = requestDTO.URL,
                EncodedHash = encoded,
                ExpiryDate = requestDTO.ExpiryDate ?? DateTime.Now.AddDays(90),
                CreatedDate = DateTime.Now,
                User = user,
            };

            await _dbContext.URLs.AddAsync(url);
            await _dbContext.SaveChangesAsync();

            return new Uri($"{context.Request.Scheme}://{context.Request.Host}/{encoded}").ToString();
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll(HttpContext context, string apiKey)
        {
            var user = await _dbContext.Users.Where(x => x.APIKey == apiKey).FirstOrDefaultAsync();

            IEnumerable<ResponseDTO> all = new List<ResponseDTO>();
            if (user == null)
            {
                throw new Exception("User is not registered");
            }
            else
            {
                var urls = await _dbContext.URLs.Where(x => x.UserId == user.Id).Select(x => new ResponseDTO
                {
                    RedirectedUrl = new Uri($"{context.Request.Scheme}://{context.Request.Host}/{x.EncodedHash}").ToString(),
                    ExpiryDate = x.ExpiryDate,
                    OriginalUrl = x.Original,
                }).ToListAsync();

                return urls;
            }
        }

        public async Task<string> GetShortUrl(HttpContext context, string hash)
        {
            var url = await _dbContext.URLs.Where(x => x.EncodedHash == hash).FirstOrDefaultAsync();
            if (url == null || url.ExpiryDate.ToLocalTime() < DateTime.Now)
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
