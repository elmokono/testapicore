using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using testapicore.Models;

namespace testapicore.Services
{
    public interface IAppointmentsService
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        void CompleteById(int id);
    }

    public class AppointmentsService : IAppointmentsService
    {
        private readonly ILogger<AppointmentsService> _logger;
        private readonly AWSTestDatabaseDBContext _dbContext;

        public AppointmentsService(AWSTestDatabaseDBContext dbContext, ILogger<AppointmentsService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IEnumerable<Appointment> GetAll()
        {
            _logger.LogInformation("reading all appointments");

            return _dbContext.Appointments
                .Include(i => i.User)
                .Include(i => i.Pacient);
        }

        public Appointment GetById(int id)
        {
            _logger.LogInformation("reading appointment {0}", id);

            return _dbContext.Appointments
                .Include(i => i.User)
                .Include(i => i.Pacient)
                .SingleOrDefault(i => i.Id == id);
        }

        public void CompleteById(int id)
        {
            _logger.LogInformation("completing appointment {0}", id);

            GetById(id).Confirmed = true;
            _dbContext.SaveChanges();
        }
    }
}
