using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using testapicore31.Models;

namespace testapicore31.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UsersService : IUsersService
    {
        private readonly ILogger<UsersService> _logger;
        private readonly AWSTestDatabaseDBContext _dbContext;

        public UsersService(AWSTestDatabaseDBContext dbContext, ILogger<UsersService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAll()
        {
            _logger.LogInformation("reading all users");

            return _dbContext.Users
                .Include(i => i.UserStatus);
        }

        public User GetById(int id)
        {
            _logger.LogInformation("reading user {0}", id);

            return _dbContext.Users
                .Include(i => i.UserStatus)
                .SingleOrDefault(i => i.Id == id);
        }

    }
}
