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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly Models.AWSTestDatabaseDBContext _dbContext;

        public UsersController(Models.AWSTestDatabaseDBContext dbContext, ILogger<UsersController> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<Models.User> Get()
        {
            try
            {
                return _dbContext.Users.Include(i => i.UserStatus);
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read users");
                return new List<Models.User>();
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<Models.User> Get(int id)
        {
            try
            {
                return _dbContext.Users.Include(i => i.UserStatus).Single(x => x.Id == id);
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read user {0}", id);
                return NotFound();
            }
        }
    }
}
