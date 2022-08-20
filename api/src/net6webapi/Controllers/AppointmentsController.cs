using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace net6webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly Repositories.IAppointmentsRepository _appointmentsService;

        public AppointmentsController(Repositories.IAppointmentsRepository appointmentsService, ILogger<UsersController> logger)
        {
            _logger = logger;
            _appointmentsService = appointmentsService;
        }

        // GET: api/<AppointmentsController>
        [HttpGet]
        public async Task<IEnumerable<Models.Appointment>> Get()
        {
            try
            {
                return await _appointmentsService.GetAll();
            }
            catch (Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read Appointments");
                return new List<Models.Appointment>();
            }
        }

        // GET api/<AppointmentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Appointment>> Get(int id)
        {
            try
            {
                return await _appointmentsService.GetById(id);
            }
            catch (Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read Appointment {id}", id);
                return NotFound();
            }
        }

        // GET api/<AppointmentsController>/5
        [HttpGet("byuser/{id}")]
        public async Task<IEnumerable<Models.Appointment>> GetByUser(int id)
        {
            try
            {
                return await _appointmentsService.GetByUserId(id);
            }
            catch (Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read Appointment for user {id}", id);
                return new List<Models.Appointment>();
            }
        }

        // PUT api/<AppointmentsController>/complete/5
        [HttpPut("checkin/{id}/{appointmentStatusId}")]
        public async Task<ActionResult<Models.Appointment>> Checkin(int id, int appointmentStatusId)
        {
            try
            {
                _appointmentsService.Checkin(id, appointmentStatusId);
                return await Get(id);
            }
            catch (Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot complete Appointment {id}", id);
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
            catch (Exception)
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
