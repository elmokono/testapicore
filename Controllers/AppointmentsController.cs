using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testapicore31.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly Services.IAppointmentsService _appointmentsService;

        public AppointmentsController(Services.IAppointmentsService appointmentsService, ILogger<UsersController> logger)
        {
            _logger = logger;
            _appointmentsService = appointmentsService;
        }

        // GET: api/<AppointmentsController>
        [HttpGet]
        public IEnumerable<Models.Appointment> Get()
        {
            try
            {
                return _appointmentsService.GetAll();
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read Appointments");
                return new List<Models.Appointment>();
            }
        }

        // GET api/<AppointmentsController>/5
        [HttpGet("{id}")]
        public ActionResult<Models.Appointment> Get(int id)
        {
            try
            {
                return _appointmentsService.GetById(id);
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read Appointment {0}", id);
                return NotFound();
            }
        }

        // GET api/<AppointmentsController>/5
        [HttpGet("byuser/{id}")]
        public IEnumerable<Models.Appointment> GetByUser(int id)
        {
            try
            {
                return _appointmentsService.GetByUserId(id);
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read Appointment for user {0}", id);
                return new List<Models.Appointment>();
            }
        }

        // PUT api/<AppointmentsController>/complete/5
        [HttpPut("complete/{id}")]
        public ActionResult<Models.Appointment> Complete(int id)
        {
            try
            {
                _appointmentsService.CompleteById(id);
                return Get(id);
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot complete Appointment {0}", id);
                throw;
            }
        }

        // POST api/<AppointmentsController>
        [HttpPost]
        public ActionResult<Models.Appointment> Post([FromBody] Models.Appointment value)
        {
            try
            {                
                return _appointmentsService.New(value);
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot create Appointment");
                throw;
            }
        }

        /*
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AppointmentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AppointmentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
