using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using testapicore31.Models;

namespace testapicore31.Repositories
{
    public interface IAppointmentsStatusRepository
    {
        IEnumerable<AppointmentStatus> GetAll();
        AppointmentStatus GetById(int id);
    }

    public class AppointmentsStatusRepository : IAppointmentsStatusRepository
    {
        private readonly ILogger<AppointmentsRepository> _logger;
        private readonly AppDBContext _dbContext;

        public AppointmentsStatusRepository(AppDBContext dbContext, ILogger<AppointmentsRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IEnumerable<AppointmentStatus> GetAll()
        {
            _logger.LogInformation("reading all appointment status");

            return _dbContext.AppointmentStatuses;
        }

        public AppointmentStatus GetById(int id)
        {
            _logger.LogInformation("reading appointment status {0}", id);

            return GetAll()
                .SingleOrDefault(i => i.Id == id);
        }
    }
}
