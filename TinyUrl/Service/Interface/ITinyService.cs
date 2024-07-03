using TinyUrl.Models.DTO;

namespace TinyUrl.Service.Interface
{
    public interface ITinyService
    {
        string CreateShortUrl(HttpContext context, string apiKey, RequestDTO requestDTO);
        string GetShortUrl(HttpContext context, string hash);
        IEnumerable<ResponseDTO> GetAll(HttpContext context, string apiKey);
        string CreateNewApiKey(string name);
    }
}
