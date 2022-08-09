using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using testapicore31.Models;

namespace testapicore31.Services
{
    public interface IAppointmentsStatusService
    {
        IEnumerable<AppointmentStatus> GetAll();
        AppointmentStatus GetById(int id);
    }

    public class AppointmentsStatusService : IAppointmentsStatusService
    {
        private readonly ILogger<AppointmentsService> _logger;
        private readonly AppDBContext _dbContext;

        public AppointmentsStatusService(AppDBContext dbContext, ILogger<AppointmentsService> logger)
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
