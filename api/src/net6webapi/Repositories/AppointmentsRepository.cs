using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using net6webapi.Models;

namespace net6webapi.Repositories
{
    public interface IAppointmentsRepository
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment> GetById(int id);
        void Checkin(int id, int appointmentStatusId);
        Appointment New(Appointment appointment);
        Task<IEnumerable<Appointment>> GetByUserId(int id);
    }

    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly ILogger<AppointmentsRepository> _logger;
        private readonly IAppointmentsStatusRepository _appointmentsStatusService;
        private readonly AppDBContext _dbContext;

        private readonly IQueryable<Appointment> _appointments;

        public AppointmentsRepository(AppDBContext dbContext, IAppointmentsStatusRepository appointmentsStatusService, ILogger<AppointmentsRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _appointmentsStatusService = appointmentsStatusService;

            _appointments = _dbContext.Appointments
                .Include(i => i.User)
                .Include(i => i.Pacient.MedicalPlan)
                .Include(i => i.AppointmentStatus);
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            _logger.LogInformation("reading all appointments");

            return await _appointments.ToListAsync();
        }

        public async Task<Appointment> GetById(int id)
        {
            _logger.LogInformation("reading appointment {id}", id);

            return await _appointments.SingleAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetByUserId(int id)
        {
            _logger.LogInformation("reading appointment for user {id}", id);

            var allAppointments = await GetAll();

            return await _appointments.Where(i => i.User.Id == id).ToListAsync();
        }

        public Appointment New(Appointment appointment)
        {
            appointment.User = _dbContext.Users.Single(x => x.Id == appointment.User.Id);
            appointment.Pacient = _dbContext.Pacients.Single(x => x.Id == appointment.Pacient.Id);

            var newAppointment = _dbContext.Add(appointment);
            _dbContext.SaveChanges();
            return newAppointment.Entity;
        }

        public async void Checkin(int id, int appointmentStatusId)
        {
            _logger.LogInformation("updating appointment {id} with status {appointmentStatusId}", id, appointmentStatusId);

            var appointmentStatus = _appointmentsStatusService.GetById(appointmentStatusId);
            var appointment = await GetById(id);
            appointment.AppointmentStatus = appointmentStatus;
            _dbContext.SaveChanges();
        }
    }
}
