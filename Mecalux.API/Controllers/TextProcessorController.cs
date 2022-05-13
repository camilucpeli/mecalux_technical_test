using Mecalux.Business.Services;
using Mecalux.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Mecalux.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TextProcessorController : ControllerBase
    {
        TextProcessorService _service;

        public TextProcessorController(TextProcessorService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<OrderOptions> GetOrderOptions()
        {

            return _service.GetOrderOptions();
        }

        [HttpGet]
        public List<string> GetOrderedText(string textToOrder, OrderOptions orderOption)
        {
            return _service.GetOrderedText(textToOrder, orderOption);
        }

        [HttpGet]
        public TextStatistics GetStatistics(string textToAnalyze)
        {
            return _service.GetStatistics(textToAnalyze);
        }
    }
}
