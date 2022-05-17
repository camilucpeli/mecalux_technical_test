using Mecalux.Business.Services;
using Mecalux.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Mecalux.API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class TextProcessorController : ControllerBase
    {
        TextProcessorService _service;

        public TextProcessorController(TextProcessorService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetOrderOptions")]
        public List<string> GetOrderOptions()
        {

            return _service.GetOrderOptions().Select(o => o.ToString()).ToList();
        }

        [HttpGet]
        [Route("GetOrderedText")]
        public List<string> GetOrderedText([FromQuery] string textToOrder, [FromQuery] string orderOption)
        {
            return _service.GetOrderedText(textToOrder, orderOption);
        }

        [HttpGet]
        [Route("GetStatistics")]
        public TextStatistics GetStatistics([FromQuery]  string textToAnalyze)
        {
            return _service.GetStatistics(textToAnalyze);
        }
    }
}
