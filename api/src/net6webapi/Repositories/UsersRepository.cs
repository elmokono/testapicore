using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using net6webapi.Models;

namespace net6webapi.Repositories
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly ILogger<UsersRepository> _logger;
        private readonly AppDBContext _dbContext;

        public UsersRepository(AppDBContext dbContext, ILogger<UsersRepository> logger)
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

            return GetAll()
                .SingleOrDefault(i => i.Id == id);
        }

    }
}
