using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testapicore31.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly Services.IUsersService _usersService;

        public UsersController(Services.IUsersService usersService, ILogger<UsersController> logger)
        {
            _logger = logger;
            _usersService = usersService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<Models.User> Get()
        {
            try
            {
                return _usersService.GetAll();
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
                return _usersService.GetById(id);
            }
            catch (System.Exception)
            {
                _logger.Log(LogLevel.Error, "Cannot read user {0}", id);
                return NotFound();
            }
        }
    }
}
