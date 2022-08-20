using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using net6webapi.Models;

namespace net6webapi.Repositories
{
    public interface IAppointmentsStatusRepository
    {
        Task<IEnumerable<AppointmentStatus>> GetAll();
        Task<AppointmentStatus?> GetById(int id);
    }

    public class AppointmentsStatusRepository : IAppointmentsStatusRepository
    {
        private readonly ILogger<AppointmentsRepository> _logger;
        private readonly AppDBContext _dbContext;
        private readonly DbSet<AppointmentStatus> _appointmentStatuses;

        public AppointmentsStatusRepository(AppDBContext dbContext, ILogger<AppointmentsRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _appointmentStatuses = _dbContext.AppointmentStatuses;
        }

        public async Task<IEnumerable<AppointmentStatus>> GetAll()
        {
            _logger.LogInformation("reading all appointment status");

            return await _appointmentStatuses.ToListAsync();
        }

        public async Task<AppointmentStatus?> GetById(int id)
        {
            _logger.LogInformation("reading appointment status {id}", id);
            
            return await _appointmentStatuses.FindAsync(id);
        }
    }
}
