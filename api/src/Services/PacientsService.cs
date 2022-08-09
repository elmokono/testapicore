﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using testapicore31.Models;

namespace testapicore31.Services
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
        private readonly AppDBContext _dbContext;

        public PacientsService(AppDBContext dbContext, ILogger<PacientsService> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IEnumerable<Pacient> GetAll()
        {
            _logger.LogInformation("reading all Pacients");
            return _dbContext.Pacients
                .Include("MedicalPlan");
        }

        public Pacient GetById(int id)
        {
            _logger.LogInformation("reading Pacient {0}", id);
            return GetAll().SingleOrDefault(i => i.Id == id);
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