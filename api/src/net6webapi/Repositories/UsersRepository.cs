using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using net6webapi.Models;

namespace net6webapi.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly ILogger<UsersRepository> _logger;
        private readonly AppDBContext _dbContext;
        private readonly DbSet<User> _users;

        public UsersRepository(AppDBContext dbContext, ILogger<UsersRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _users = _dbContext.Users;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            _logger.LogInformation("reading all users");

            return await _users.Include(i => i.UserStatus).ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            _logger.LogInformation("reading user {id}", id);

            return await _users.Include(i => i.UserStatus).SingleOrDefaultAsync(i => i.Id == id);
        }

    }
}
