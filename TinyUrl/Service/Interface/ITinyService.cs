using TinyUrl.Models.DTO;

namespace TinyUrl.Service.Interface
{
    public interface ITinyService
    {
        Task<string> CreateShortUrl(HttpContext context, string apiKey, RequestDTO requestDTO);
        Task<string> GetShortUrl(HttpContext context, string hash);
        Task<IEnumerable<ResponseDTO>> GetAll(HttpContext context, string apiKey);
        Task<string> CreateNewApiKey(string name);
    }
}
