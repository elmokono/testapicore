using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testapicore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly Models.AWSTestDatabaseDBContext _dbContext;
        private readonly Services.IAppointmentsService _appointmentsService;

        public AppointmentsController(Models.AWSTestDatabaseDBContext dbContext, Services.IAppointmentsService appointmentsService, ILogger<UsersController> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
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

        // PUT api/<AppointmentsController>/5
        [HttpGet("complete/{id}")]
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

        /*
        // POST api/<AppointmentsController>
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
