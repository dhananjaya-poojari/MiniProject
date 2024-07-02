using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TinyUrl.Models.DTO;
using TinyUrl.Service.Interface;

namespace TinyUrl.Controllers
{
    [Route("/")]
    [ApiController]
    public class TinyUrlController : ControllerBase
    {
        private readonly ITinyService _service;
        public TinyUrlController(ITinyService service)
        {
            _service = service;
        }

        [HttpGet("{hash}")]
        public async Task<IActionResult> Get(string hash)
        {
            var url = await _service.GetShortUrl(HttpContext, hash);
            if (string.IsNullOrEmpty(url)) { return NotFound(); }
            return Redirect(url);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<ResponseDTO>> GetAll([FromHeader(Name = "apiKey")][Required] string requiredHeader)
        {
            return await _service.GetAll(HttpContext, requiredHeader);
        }

        [HttpPost]
        public async Task<string> Post([FromHeader(Name = "apiKey")][Required] string requiredHeader, [FromBody] RequestDTO requestDTO)
        {
            string responseDTO = await _service.CreateShortUrl(HttpContext, requiredHeader, requestDTO);
            return responseDTO;
        }

        [HttpPost("apiKey")]
        public async Task<string> CreateNewApiKey([FromBody] string name)
        {
            string res = await _service.CreateNewApiKey(name);
            return res;
        }
    }
}
