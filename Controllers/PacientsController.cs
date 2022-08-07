using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testapicore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientsController : ControllerBase
    {
        private readonly ILogger<PacientsController> _logger;
        private readonly Services.IPacientsService _pacientsService;

        public PacientsController(Services.IPacientsService pacientsService, ILogger<PacientsController> logger)
        {
            _logger = logger;
            _pacientsService = pacientsService;
        }

        // GET: api/<PacientsController>
        [HttpGet]
        public IEnumerable<Models.Pacient> Get()
        {
            try
            {
                return _pacientsService.GetAll();
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read Pacients");
                return new List<Models.Pacient>();
            }
        }

        // GET api/<PacientsController>/5
        [HttpGet("{id}")]
        public ActionResult<Models.Pacient> Get(int id)
        {
            try
            {
                return _pacientsService.GetById(id);
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read Pacient {0}", id);
                return NotFound();
            }
        }

        // POST api/<PacientsController>
        [HttpPost]
        public ActionResult<Models.Pacient> Post([FromBody] Models.Pacient value)
        {
            try
            {
                return _pacientsService.New(value);
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot create Pacient");
                throw;
            }
        }
    }
}
