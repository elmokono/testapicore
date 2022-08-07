using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using testapinet6.Models;

namespace testapinet6.Services
{
    public interface IAppointmentsService
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        void CompleteById(int id);
        Appointment New(Appointment appointment);
        IEnumerable<Appointment> GetByUserId(int id);
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

        public IEnumerable<Appointment> GetByUserId(int id)
        {
            _logger.LogInformation("reading appointment for user {0}", id);

            return _dbContext.Appointments
                .Include(i => i.User)
                .Include(i => i.Pacient)
                .Where(i => i.User.Id == id);
        }

        public Appointment New(Appointment appointment)
        {
            appointment.User = _dbContext.Users.Single(x => x.Id == appointment.User.Id);
            appointment.Pacient = _dbContext.Pacients.Single(x => x.Id == appointment.Pacient.Id);

            var newAppointment = _dbContext.Add(appointment);
            _dbContext.SaveChanges();
            return newAppointment.Entity;
        }

        public void CompleteById(int id)
        {
            _logger.LogInformation("completing appointment {0}", id);

            GetById(id).Confirmed = true;
            _dbContext.SaveChanges();
        }
    }
}
