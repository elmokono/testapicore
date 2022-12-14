using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace net6webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientsController : ControllerBase
    {
        private readonly ILogger<PacientsController> _logger;
        private readonly Repositories.IPacientsRepository _pacientsService;

        public PacientsController(Repositories.IPacientsRepository pacientsService, ILogger<PacientsController> logger)
        {
            _logger = logger;
            _pacientsService = pacientsService;
        }

        // GET: api/<PacientsController>
        [HttpGet]
        public async Task<IEnumerable<Models.Pacient>> Get()
        {
            try
            {
                return await _pacientsService.GetAll();
            }
            catch (Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read Pacients");
                return new List<Models.Pacient>();
            }
        }

        // GET api/<PacientsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Pacient>> Get(int id)
        {
            try
            {
                var pacient = await _pacientsService.GetById(id);
                if (pacient == null) return NotFound();

                return pacient;
            }
            catch (Exception)
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
            catch (Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot create Pacient");
                throw;
            }
        }
    }
}
