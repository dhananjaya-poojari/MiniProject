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
        public IActionResult Get(string hash)
        {
            var url = _service.GetShortUrl(HttpContext, hash);
            if (string.IsNullOrEmpty(url)) { return NotFound(); }
            return Redirect(url);
        }

        [HttpGet("all")]
        public IEnumerable<ResponseDTO> GetAll([FromHeader(Name = "apiKey")][Required] string requiredHeader)
        {
            return _service.GetAll(HttpContext, requiredHeader);
        }

        [HttpPost]
        public string Post([FromHeader(Name = "apiKey")][Required] string requiredHeader, [FromBody] RequestDTO requestDTO)
        {
            string responseDTO = _service.CreateShortUrl(HttpContext, requiredHeader, requestDTO);
            return responseDTO;
        }

        [HttpPost("apiKey")]
        public string CreateNewApiKey([FromBody] UserDTO userDTO)
        {
            string res = _service.CreateNewApiKey(userDTO.Email);
            return res;
        }
    }

    public record UserDTO(string Email);

}
