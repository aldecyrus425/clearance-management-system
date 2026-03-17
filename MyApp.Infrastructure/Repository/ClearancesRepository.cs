using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces.Repository;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repository
{
    public class ClearancesRepository : IClearancesRespository
    {

        private readonly ApplicationDBContext _dbContext;
        public ClearancesRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Clearances> AddClearanceAsync(Clearances clearance)
        {
            await _dbContext.Clearances.AddAsync(clearance);
            await _dbContext.SaveChangesAsync();
            return clearance;
        }

        public async Task<Clearances?> GetClearanceByIdAsync(int id)
        {
            return await _dbContext.Clearances
                .Include(c => c.Students)
                .Include(c => c.SchoolYears)
                .FirstOrDefaultAsync(c => c.ClearanceId == id);
        }

        public async Task<IEnumerable<Clearances>> GetAllClearancesAsync()
        {
            return await _dbContext.Clearances
                .AsNoTracking()
                .Include(c => c.Students)
                .Include(c => c.SchoolYears)
                .ToListAsync();
        }

        public async Task<IEnumerable<Clearances>> GetClearancesByStudentAsync(int studentId)
        {
            return await _dbContext.Clearances
                .Where(c => c.StudentId == studentId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> RemoveClearanceAsync(int id)
        {
            var clearance = await _dbContext.Clearances
                .FirstOrDefaultAsync(c => c.ClearanceId == id);

            if (clearance == null)
                return false;

            _dbContext.Clearances.Remove(clearance);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
