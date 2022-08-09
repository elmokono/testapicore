using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using testapicore31.Models;

namespace testapicore31.Services
{
    public interface IAppointmentsService
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        void Checkin(int id, int appointmentStatusId);
        Appointment New(Appointment appointment);
        IEnumerable<Appointment> GetByUserId(int id);
    }

    public class AppointmentsService : IAppointmentsService
    {
        private readonly ILogger<AppointmentsService> _logger;
        private readonly IAppointmentsStatusService _appointmentsStatusService;
        private readonly AppDBContext _dbContext;

        public AppointmentsService(AppDBContext dbContext, IAppointmentsStatusService appointmentsStatusService, ILogger<AppointmentsService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _appointmentsStatusService = appointmentsStatusService;
        }

        public IEnumerable<Appointment> GetAll()
        {
            _logger.LogInformation("reading all appointments");

            return _dbContext.Appointments
                .Include(i => i.User)
                .Include(i => i.Pacient)
                .Include(i => i.AppointmentStatus);
        }

        public Appointment GetById(int id)
        {
            _logger.LogInformation("reading appointment {0}", id);

            return GetAll()
                .SingleOrDefault(i => i.Id == id);
        }

        public IEnumerable<Appointment> GetByUserId(int id)
        {
            _logger.LogInformation("reading appointment for user {0}", id);

            return GetAll()
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

        public void Checkin(int id, int appointmentStatusId)
        {
            _logger.LogInformation("updating appointment {0} with status {1}", id, appointmentStatusId);

            var appointmentStatus = _appointmentsStatusService.GetById(appointmentStatusId);
            GetById(id).AppointmentStatus = appointmentStatus;
            _dbContext.SaveChanges();
        }
    }
}
