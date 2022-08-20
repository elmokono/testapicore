using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using net6webapi.Models;

namespace net6webapi.Repositories
{
    public interface IPacientsRepository
    {
        Task<IEnumerable<Pacient>> GetAll();
        Task<Pacient?> GetById(int id);
        Pacient New(Pacient appointment);
    }

    public class PacientsRepository : IPacientsRepository
    {
        private readonly ILogger<PacientsRepository> _logger;
        private readonly AppDBContext _dbContext;
        private readonly DbSet<Pacient> _pacients;

        public PacientsRepository(AppDBContext dbContext, ILogger<PacientsRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _pacients = _dbContext.Pacients;
        }

        public async Task<IEnumerable<Pacient>> GetAll()
        {
            _logger.LogInformation("reading all Pacients");
            return await _pacients.Include("MedicalPlan").ToListAsync();
        }

        public async Task<Pacient?> GetById(int id)
        {
            _logger.LogInformation("reading Pacient {id}", id);
            return await _pacients.Include("MedicalPlan").SingleOrDefaultAsync(x => x.Id == id);
        }

        public Pacient New(Pacient pacient)
        {
            pacient.MedicalPlan = _dbContext.MedicalPlans.Single(x => x.Id == pacient.MedicalPlan.Id);
            var newPacient = _dbContext.Add(pacient);
            _dbContext.SaveChanges();
            return newPacient.Entity;
        }
    }
}
