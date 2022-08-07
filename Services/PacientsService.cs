using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using testapinet6.Models;

namespace testapinet6.Services
{
    public interface IPacientsService
    {
        IEnumerable<Pacient> GetAll();
        Pacient GetById(int id);
        Pacient New(Pacient appointment);
    }

    public class PacientsService : IPacientsService
    {
        private readonly ILogger<PacientsService> _logger;
        private readonly AWSTestDatabaseDBContext _dbContext;

        public PacientsService(AWSTestDatabaseDBContext dbContext, ILogger<PacientsService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IEnumerable<Pacient> GetAll()
        {
            _logger.LogInformation("reading all Pacients");

            return _dbContext.Pacients;
        }

        public Pacient GetById(int id)
        {
            _logger.LogInformation("reading Pacient {0}", id);

            return _dbContext.Pacients
                .SingleOrDefault(i => i.Id == id);
        }

        public Pacient New(Pacient pacient)
        {
            var newPacient = _dbContext.Add(pacient);
            _dbContext.SaveChanges();
            return newPacient.Entity;
        }
    }
}
