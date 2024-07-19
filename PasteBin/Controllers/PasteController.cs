using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Service;

namespace Server.Controllers
{
    [ApiController]
    public class PasteController : ControllerBase
    {
        private readonly IPasteService _service;
        public PasteController(IPasteService pasteService)
        {
            _service = pasteService;
        }

        [HttpPost("/upload")]
        public string Upload(PasteDTO pasteDTO)
        {
            return _service.Upload(HttpContext, pasteDTO.Content);
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            return _service.Get(id);
        }
    }

    public record PasteDTO
    {
        public string Content { get; set; }
    }
}
